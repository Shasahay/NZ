using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesZone.Web.Controllers
{
    using NotesZone.DataAccess.Repository.Basket;
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.DataAccess.Repository.Order;
    using NotesZone.Domain.Address;
    using NotesZone.Domain.Basket;
    using NotesZone.Domain.Common;
    using NotesZone.Domain.Factory;
    using NotesZone.Domain.User;
    using NotesZone.Web.Infra;
    using NotesZone.Web.Infra.ViewModels.Persistences;
    using NotesZone.Web.ViewModels;
    public class BasketController : BaseController
    {
        private IBasketRepository _basketRepository;
        private IItemRepository _itemRepository;
        private IOrderProcessRepository _orderProcessRepository;
        private IUserSubscriptionPersistence _userSubscriptionPersistence;
        public BasketController()
        {
            _basketRepository = new BasketRepository();
            _itemRepository = new ItemRepository();
            _orderProcessRepository = new OrderProcessRepository();
        }
        // GET: Basket
        public ActionResult Index( string returnUrl, Basket basket)
        {
            return View(new BasketIndexViewModel { Basket = basket, ReturnUrl = returnUrl });

        }

        public RedirectToRouteResult AddToBasket(Basket basket, string returnUrl, string itemContentID)
        {
            //var contentID = (itemContentID == null) ? returnUrl.Split('/')[3] : itemContentID;
            ItemContent itemContent = _itemRepository.GetItemContents().FirstOrDefault(it => it.Id == int.Parse(itemContentID));
            if( itemContent != null)
            {
                basket.AddItem(itemContent, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromBasket(Basket basket, string returnUrl, string itemContentID)
        {
            //var contentID = (itemContentID == null) ? returnUrl.Split('/')[3] : itemContentID;
            ItemContent itemContent = _itemRepository.GetItemContents().FirstOrDefault(it => it.Id == int.Parse(itemContentID));
            if (itemContent != null)
            {
                basket.RemowItem(itemContent);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult BasketSummary(Basket basket)
        {
            return PartialView(basket);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(Basket basket, ShippingDetails shippingDetails, string returnUrl)
        {
            //var user = (User)(Session["User"]);
            var user = this.GetUser();
            if (string.IsNullOrEmpty(user.Email))
            {
                return RedirectToAction("LogIn", "Account",  new { returnUrl });
            }
            else
            {
                if (basket.Lines.Count() == 0)
                {
                    ModelState.AddModelError("", "Sorry, your cart is empty!");
                }
                if (ModelState.IsValid)  
                {
                    // write condition also to check for successfull payment completed if true then below code should execute
                    var orderItemContents = (List<ItemContent>)(basket.Lines.Select(x => x.ItemContent).Distinct().ToList());
                    _userSubscriptionPersistence = new UserSubscriptionPersistence(); // Violating deendency injection principle; will take care latar
                    var subscription = new Subscription { SubscriptionName = "Download" };
                    var userSubscriptions = UserSubscriptionFactory.CreateUserSubscription(user, orderItemContents, subscription);
                    var isSuccess = _userSubscriptionPersistence.PersistenceUserSubscription(userSubscriptions);
                    if (isSuccess)
                    {
                        _orderProcessRepository.EmailProcessOrder(basket, shippingDetails);
                        basket.Clear();
                        return View("Completed");
                    }

                    else
                    {
                        // put your failure logic
                        return View("Error");
                    }
                }
                else
                {
                    return View(shippingDetails);
                }
                
            }
           
        }
        /// <summary>
        /// Comment this method; since we are now using model binder
        /// </summary>
        /// <returns></returns>
        private Basket GetBasket()
        {
            Basket basket = (Basket)Session["Basket"];
            if(basket == null )
            {
                basket = new Basket();
                Session["Basket"] = basket;
            }
            return basket;
        }
    }
}