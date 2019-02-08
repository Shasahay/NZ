using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesZone.Web.Controllers
{
    using NotesZone.Domain.Common;
    using NotesZone.Domain.Factory;
    using NotesZone.Domain.User;
    using NotesZone.Web.Infra;
    using NotesZone.Web.Infra.ActionResults.Client;
    using NotesZone.Web.Infra.MediaItem;
    using NotesZone.Web.Infra.ViewModels;
    using NotesZone.Web.Infra.ViewModels.Persistences;
    using NotesZone.Web.Infra.Extensions;
using NotesZone.DataAccess.Repository.Common;
    public class UploadDashboardController : BaseController
    {
        private  IItemCreatingPersistence _itemCreatingPersistence;
        private  IMediaItemStorage _itemStorage;
        private IUserSubscriptionPersistence _userSubscriptionPersistence;
        private IItemRepository _ItemRepository;
        private ICategoryRepository _categoryRepository;
        public UploadDashboardController(IItemCreatingPersistence itemCreatingPersistence, IMediaItemStorage itemStorage)
        {
            _itemCreatingPersistence = itemCreatingPersistence;
            _itemStorage = itemStorage;
        }
        public UploadDashboardController()
        {
            _itemStorage = new MediaItemStorage();
        }
        // GET: UploadDocument
        public ActionResult Index(User user)
        {
            if (!string.IsNullOrEmpty(user.Email))  // later will add condition also string.IsNullOrEmpty(user.Username)
            {
                return new UploadDashboardViewModelActionResult<UploadDashboardController>(x => x.Index(user));
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl =  Request.RawUrl });
            }
        }
        [HttpPost]
        public ActionResult UploadDashboard(HttpPostedFileBase file, ItemContentViewModel itemContentViewModel, string Category, string SubCategory1)
        {
            this._itemCreatingPersistence = new ItemCreatingPersistence();// Violating deendency injection principle; will take care latar
            this._categoryRepository = new CategoryRepository();// Violating deendency injection principle; will take care latar
            this._userSubscriptionPersistence = new UserSubscriptionPersistence(); // Violating deendency injection principle; will take care latar
            this._ItemRepository = new ItemRepository();
            this._itemStorage = new MediaItemStorage();
            var item = CreateOrUpdateItem(itemContentViewModel, true);
            //var user = (User)(Session["User"]);
            var itemKey = item.ItemContent.ItemCategoryKey;
            if (_itemCreatingPersistence.PersistenceItem(item) && _itemCreatingPersistence.PersistenceItemCategory(CreateOrUpdateItemCategory(itemKey, Category, SubCategory1, true)));
            {

                var itmCnt = _ItemRepository.GetItemContents(itemKey);
                if(itmCnt != null)
                {
                    var userSubscription = CreateOrUpdateUserSubscription(this.GetUser(), itmCnt);
                    _userSubscriptionPersistence.PersistenceUserSubscription(userSubscription);
                         ViewBag.Message = itemKey;
                    return View("SuccessSubmission");
                }
                // SucceedMessage("Save item successfully"); // inherit Base controller
                //else
                //ErrorMessage("Cannot create item");
                else
                {
                    return null;
                }

                //return RedirectToAction("Index", "UploadDashboard");
                
            }
          
        }

        private Item CreateOrUpdateItem(dynamic vm, bool isNew)
        {
            var smallImagePath = string.Empty;
            var mediumImagePath = string.Empty;
            var largeImagePath = string.Empty;
            var documentFilePath = string.Empty;
            var itemContent = ItemContentFactory.CreateItemContent(
                                  vm.ItemContent.Title,
                                  vm.ItemContent.SortDescription,
                                  vm.ItemContent.Content,
                                  smallImagePath,
                                  this.GetUserEmail()
                //mediumImagePath,
                //largeImagePath
                                  );
            var itemCategoryID = _categoryRepository.GetCategories().Where(x => x.Name.Equals("Notes", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Id).FirstOrDefault();  // This is a bad coading practices
            // is New is checking for editing or new creating item
            Item item = isNew
                ? ItemFactory.CreateItem(this.GetUserEmail(), itemContent, itemCategoryID)
                        : ItemFactory.CreateItem(vm.ItemContent.ItemId, this.GetUserEmail(), itemContent);
            if (vm.SmallImage != null)
            {   
                //(vm.SmallImage).FileName = string.Concat( (vm.DocumentFile).FileName,"_", item.ItemContent.ItemCategoryKey);
                smallImagePath = ((HttpPostedFileBase)vm.SmallImage).CreateImagePathFromStream(_itemStorage, item.ItemContent.ItemCategoryKey.ToString(), item.ItemContent.Title);
            }
            if(vm.DocumentFile != null)
            {
                //((HttpPostedFileBase)vm.DocumentFile).FileName = string.Concat(((HttpPostedFileBase)vm.DocumentFile).FileName, "_", item.ItemContent.ItemCategoryKey);
                documentFilePath = ((HttpPostedFileBase)vm.DocumentFile).CreateDocumentFilePathFromStream(_itemStorage, item.ItemContent.ItemCategoryKey.ToString(), item.ItemContent.Title);
            }

            //if (vm.MediumImage != null)
            //{
            //    mediumImagePath = vm.MediumImage.CreateImagePathFromStream(_itemStorage);
            //}

            //if (vm.BigImage != null)
            //{
            //    largeImagePath = vm.BigImage.CreateImagePathFromStream(_itemStorage);
            //}

            //var category = CategoryFactory.CreateCategory(vm.CategoryId);
            
            item.ItemContent.FileName = documentFilePath;
            item.ItemContent.SmallImage = smallImagePath;
            return item;
            
        }

        private UserSubscription CreateOrUpdateUserSubscription( User user, ItemContent itemContent)
        {
            var subscription = new Subscription { SubscriptionName = "Upload" };
            return UserSubscriptionFactory.CreateUserSubscription(user, itemContent, subscription);
        }

        private ItemCategory CreateOrUpdateItemCategory(Guid itemKey, string Category, string SubCategory1,  bool isNew)
        {
           
            // is New is checking for editing or new creating item
            ItemCategory itemCategory = isNew
                        ? ItemCategoryFactory.CreateItemCategory(itemKey,this.GetUserEmail(), Category, SubCategory1)
                        : ItemCategoryFactory.CreateItemCategory();

            return itemCategory;

        }

    }
}