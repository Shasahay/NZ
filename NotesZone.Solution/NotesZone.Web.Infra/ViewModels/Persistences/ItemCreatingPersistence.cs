using NotesZone.DataAccess.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels.Persistences
{
    using NotesZone.DataAccess.Repository.Common;
    using NotesZone.DataAccess.Repository.User;
    using NotesZone.Domain.Common;
    using System.Data;
    using System.Web.Mvc;
    public class ItemCreatingPersistence : IItemCreatingPersistence
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public ItemCreatingPersistence()
            : this(DependencyResolver.Current.GetService<IItemRepository>(),
                    DependencyResolver.Current.GetService<ICategoryRepository>(),
                    DependencyResolver.Current.GetService<IUserRepository>())
        {
        }

        public ItemCreatingPersistence(IItemRepository itemRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            this._itemRepository = new ItemRepository(); ;
            this._categoryRepository = new CategoryRepository();
            this._userRepository = new UserRepository(); ;

        }
        public bool PersistenceItem(Item item)
        {
            //var category = this._categoryRepository.GetCategoryById(item.Category.Id);
            //var category = this._categoryRepository.GetCategories().Where(x => x.Name == "Notes").FirstOrDefault(); // uploading for noted category only
            //if (category != null)
            //item.Category = category;
            return this._itemRepository.SaveItem(item);
        }

        public bool PersistenceItemCategory(ItemCategory itemcategory)
        {
            return this._itemRepository.SaveItemCategory(itemcategory);
            
        }
    }
}
