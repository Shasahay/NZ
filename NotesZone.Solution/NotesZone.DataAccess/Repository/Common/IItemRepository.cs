using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Common
{ 
    using NotesZone.Domain.Common;
    public interface IItemRepository
    {
        List<Item> GetItems();
        List<Item> GetItems(int index, int numOfpage, out int numOfRecords);
        List<Item> GetMostViews(int numOfPage, int numOfItem = 5);
        List<Item> GetMostDownload(int numOfPage, int numOfItem = 5);
        List<Item> GetLatestItem(int numOfPage);
        List<Item> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords);
        List<Item> GetItemByCategory(int categoryId);
        List<Item> GetItemByCategory(int index, int numOfpage, out int numOfRecords, int categoryId, string searchcategory = null, string searchsubcategory1 = null);
        List<Item> GetItemBysearchcategory(int categoryId, string searchCategory, string searchSubCategory1);
        Item GetById(int id);
        List<ItemContent> GetItemContents();
        ItemContent GetItemContents(Guid itemCategoryKey);
        List<ItemContent> GetItemContents(string itemCategory, string itemSubCategory, string aurthor);
        List<ItemContent> GetItemContentByAuthor(string author);
        List<ItemContent> GetItemContentByItemCategory( string itemCategory);
        List<ItemContent> GetItemContentByItemSubCategory(string itemSubCategory);
        List<ItemContent> GetItemContentByItemCategory(string itemCategory, string itemSubCategory);
        List<ItemCategory> GetItemCategories();
        ItemCategory GetItemCategory();
        bool SaveItem(Item item);
        bool SaveItemCategory(ItemCategory itemCategory);
        bool SaveNumberofView(int itemID);
        
    }
}
