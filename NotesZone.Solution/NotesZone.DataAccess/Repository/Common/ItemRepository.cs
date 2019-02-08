using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Repository.Common
{
    using NotesZone.DataAccess.UnitOfWork;
    using NotesZone.Domain.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public class ItemRepository : Repository<Item>, IItemRepository
    {

        public List<Item> GetItems()
        {
            List<Item> lstItems = new List<Item>();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemColle = dbContext.Items.Join
                    (dbContext.ItemContents.Where (i => i.IsActive && !i.IsDeleted),
                    item => item.ItemContentId,
                    itemcontent => itemcontent.Id,
                    (item, itemcontent) => new { item, itemcontent }
                    ).ToList();
                foreach (var element in itemColle)
                {
                    lstItems.Add(new Item { Id = element.item.Id, ItemContent = element.itemcontent });
                }

            }
            return lstItems;
        }

        public List<Item> GetItems(int index, int numOfpage, out int numOfRecords)
        {
            var items = GetItems();
            numOfRecords = items.Count();
            return items.Skip((index - 1) * numOfpage).Take(numOfpage).ToList();
        }

        public List<Item> GetLatestItem(int numOfItem)
        {
            var items = GetItems();
            return items.OrderByDescending(d => d.CreatedDate).Take(numOfItem).ToList();
        }

        public List<Item> GetMostViews(int numOfPage, int numOfItem)
        {
            List<Item> lstItems = new List<Item>();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemColle = dbContext.ItemContents.Join
                    (dbContext.ItemRecords,
                    itemContent => itemContent.Id,
                    itemRecord => itemRecord.ItemContentId,
                    (itemContent, itemRecord) => new { itemContent, itemRecord }
                    ).Join
                    (dbContext.Items,
                        itmContent => itmContent.itemContent.Id,
                        item => item.ItemContentId,
                        (itmContent, item) => new { itmContent, item }
                    ).OrderByDescending(x => x.itmContent.itemRecord.NumOfView).Take(numOfItem).ToList();

                    
                    // dbContext.Items.Join
                    //(dbContext.ItemContents,
                    //item => item.ItemContentId,
                    //itemcontent => itemcontent.Id,
                    //(item, itemcontent) => new { item, itemcontent }
                    //).Join
                    //(dbContext.ItemRecords,
                    // itmContent => itmContent.itemcontent.Id,
                    // itemRecord => itemRecord.ItemContentId,
                    // (item, itmContent) => new { }
                    //).OrderByDescending(i => i.itemcontent.NumOfView).Take(5).ToList(); // Only first five items
                foreach (var element in itemColle)
                {
                    lstItems.Add(new Item { Id = element.item.Id, ItemContent =  element.itmContent.itemContent});
                }

            }
            return lstItems;
        }


        public List<Item> GetMostDownload(int numOfPage, int numOfItem)
        {
            List<Item> lstItems = new List<Item>();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemColle = dbContext.ItemContents.Join
                    (dbContext.ItemRecords,
                    itemContent => itemContent.Id,
                    itemRecord => itemRecord.ItemContentId,
                    (itemContent, itemRecord) => new { itemContent, itemRecord }
                    ).Join
                    (dbContext.Items,
                        itmContent => itmContent.itemContent.Id,
                        item => item.ItemContentId,
                        (itmContent, item) => new { itmContent, item }
                    ).OrderByDescending(x => x.itmContent.itemRecord.NumOfDownload).Take(numOfItem).ToList();


                // dbContext.Items.Join
                //(dbContext.ItemContents,
                //item => item.ItemContentId,
                //itemcontent => itemcontent.Id,
                //(item, itemcontent) => new { item, itemcontent }
                //).Join
                //(dbContext.ItemRecords,
                // itmContent => itmContent.itemcontent.Id,
                // itemRecord => itemRecord.ItemContentId,
                // (item, itmContent) => new { }
                //).OrderByDescending(i => i.itemcontent.NumOfView).Take(5).ToList(); // Only first five items
                foreach (var element in itemColle)
                {
                    lstItems.Add(new Item { Id = element.item.Id, ItemContent = element.itmContent.itemContent });
                }

            }
            return lstItems;
        }

        public List<Item> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetItemByCategory(int categoryId)
        {
            #region Working code;
            //implement rating :- but not to make join with ItemRecode table otherwiase itemReocrd tabls also updated accoringly
            //List<Item> lstItems = new List<Item>();
            //using (var dbContext = new NotesZoneDBContext())
            //{
            //    var itemColle = dbContext.Items.Where(i => i.ItemCategoryId == categoryId).Join
            //        (dbContext.Categories,
            //        item => item.Category.Id,
            //        category => category.Id,
            //        (item, category) => new { item, category }
            //        ).Join
            //        (dbContext.ItemContents.Where(ic => ic.IsActive && !ic.IsDeleted),
            //        it => it.item.ItemContentId,
            //        ic => ic.Id,
            //        (it, ic) => new { it, ic }
            //        ).Join
            //        (dbContext.ItemRecords,
            //            itmContent => itmContent.ic.Id,
            //            itmRecord => itmRecord.ItemContentId,
            //            (itmContent, itmRecord) => new { itmContent, itmRecord }
            //        );

            //    IList<ItemRecord> lstRecord = new List<ItemRecord>();
            //    foreach (var element in itemColle)
            //    {
            //        lstItems.Add(new Item
            //        {
            //            Id = element.itmContent.it.item.Id,
            //            ItemContent = new ItemContent
            //            {
            //                Id = element.itmContent.ic.Id,
            //                Title = element.itmContent.ic.Title,
            //                Author = element.itmContent.ic.Author,
            //                BigImage = element.itmContent.ic.BigImage,
            //                SmallImage = element.itmContent.ic.SmallImage,
            //                CreatedBy = element.itmContent.ic.CreatedBy,
            //                Content = element.itmContent.ic.Content,
            //                SortDescription = element.itmContent.ic.SortDescription,
            //                Price = element.itmContent.ic.Price,
            //                FileName = element.itmContent.ic.FileName,
            //                ItemRecord =   new List<ItemRecord> { new ItemRecord { Rating = element.itmRecord.Rating }}

            //            },
            //            Category = element.itmContent.it.category
            //        });
            //    }

            //}
            //return lstItems; 
            #endregion

            List<Item> lstItems = new List<Item>();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemColle = dbContext.Items.Where(i => i.ItemCategoryId == categoryId).Join
                    (dbContext.Categories,
                    item => item.Category.Id,
                    category => category.Id,
                    (item, category) => new { item, category }
                    ).Join
                    (dbContext.ItemContents.Where(ic => ic.IsActive && !ic.IsDeleted),
                    it => it.item.ItemContentId,
                    ic => ic.Id,
                    (it, ic) => new { it, ic }
                    );


                foreach (var element in itemColle)
                {
                    lstItems.Add(new Item
                    {
                        Id = element.it.item.Id,
                        ItemContent = new ItemContent
                        {
                            Id = element.ic.Id,
                            Title = element.ic.Title,
                            Author = element.ic.Author,
                            BigImage = element.ic.BigImage,
                            SmallImage = element.ic.SmallImage,
                            CreatedBy = element.ic.CreatedBy,
                            Content = element.ic.Content,
                            SortDescription = element.ic.SortDescription,
                            Price = element.ic.Price,
                            FileName = element.ic.FileName

                        },
                        Category = element.it.category
                    });
                }
            }
            return lstItems;

        }
        private List<Item> GetItemBysearchcategory(int categoryId, string searchCategory)
        {
            List<Item> lstItems = new List<Item>();
            
            using (var dbContext = new NotesZoneDBContext())
            {
                
                var itemColle = dbContext.Items.Where(i => i.ItemCategoryId == categoryId).Join
                    (dbContext.Categories,
                    item => item.Category.Id,
                    category => category.Id,
                    (item, category) => new { item, category }
                    ).Join
                    (dbContext.ItemContents.Where(ic => ic.IsActive && !ic.IsDeleted),
                    it => it.item.ItemContentId,
                    ic => ic.Id,
                    (it, ic) => new { it, ic }
                    ).Join
                    (dbContext.ItemCategory.Where(x=> x.ItemCategoryName.Equals(searchCategory, StringComparison.CurrentCultureIgnoreCase)),
                    itmC => itmC.ic.ItemCategoryKey,
                    itmCat => itmCat.ItemCategoryKey,
                    (itmC, itmCat) => new {itmC, itmCat}
                    );
                foreach (var element in itemColle)
                {
                    lstItems.Add(new Item
                    {
                        Id = element.itmC.it.item.Id,
                        ItemContent = new ItemContent
                        {
                            Id = element.itmC.ic.Id,
                            Title = element.itmC.ic.Title,
                            Author = element.itmC.ic.Author,
                            BigImage = element.itmC.ic.BigImage,
                            SmallImage = element.itmC.ic.SmallImage,
                            CreatedBy = element.itmC.ic.CreatedBy,
                            Content = element.itmC.ic.Content,
                            SortDescription = element.itmC.ic.SortDescription,
                            Price = element.itmC.ic.Price,
                            FileName = element.itmC.ic.FileName

                        },
                        Category = element.itmC.it.category
                    });
                }

            }
            return lstItems;
            
        }

        private List<Item> GetItemBysearchsearchSubCategory1(int categoryId, string searchSubCategory1)
        {
            List<Item> lstItems = new List<Item>();
            using (var dbContext = new NotesZoneDBContext())
            {
                var itemColle = dbContext.Items.Where(i => i.ItemCategoryId == categoryId).Join
                        (dbContext.Categories,
                        item => item.Category.Id,
                        category => category.Id,
                        (item, category) => new { item, category }
                        ).Join
                        (dbContext.ItemContents.Where(ic => ic.IsActive && !ic.IsDeleted),
                        it => it.item.ItemContentId,
                        ic => ic.Id,
                        (it, ic) => new { it, ic }
                        ).Join
                        (dbContext.ItemCategory.Where(x => x.ItemSubCategory1.Equals(searchSubCategory1, StringComparison.CurrentCultureIgnoreCase)),
                        itmC => itmC.ic.ItemCategoryKey,
                        itmCat => itmCat.ItemCategoryKey,
                        (itmC, itmCat) => new { itmC, itmCat }
                        );

                foreach (var element in itemColle)
                {
                    lstItems.Add(new Item
                    {
                        Id = element.itmC.it.item.Id,
                        ItemContent = new ItemContent
                        {
                            Id = element.itmC.ic.Id,
                            Title = element.itmC.ic.Title,
                            Author = element.itmC.ic.Author,
                            BigImage = element.itmC.ic.BigImage,
                            SmallImage = element.itmC.ic.SmallImage,
                            CreatedBy = element.itmC.ic.CreatedBy,
                            Content = element.itmC.ic.Content,
                            SortDescription = element.itmC.ic.SortDescription,
                            Price = element.itmC.ic.Price,
                            FileName = element.itmC.ic.FileName

                        },
                        Category = element.itmC.it.category
                    });
                }

            }
            return lstItems;
        }
        public List<Item> GetItemBysearchcategory(int categoryId, string searchCategory, string searchSubCategory1)
        {
            List<Item> lstItems = new List<Item>();
            if (string.IsNullOrEmpty(searchSubCategory1) && !string.IsNullOrEmpty(searchCategory) )

                return GetItemBysearchcategory(categoryId, searchCategory);

            if (string.IsNullOrEmpty(searchCategory) && !string.IsNullOrEmpty(searchSubCategory1) )
                return  GetItemBysearchsearchSubCategory1(categoryId, searchSubCategory1);
            if (!string.IsNullOrEmpty(searchCategory) && !string.IsNullOrEmpty(searchSubCategory1))
            {
                
                using (var dbContext = new NotesZoneDBContext())
                {
                    var itemColle = dbContext.Items.Where(i => i.ItemCategoryId == categoryId).Join
                            (dbContext.Categories,
                            item => item.Category.Id,
                            category => category.Id,
                            (item, category) => new { item, category }
                            ).Join
                            (dbContext.ItemContents.Where(ic => ic.IsActive && !ic.IsDeleted),
                            it => it.item.ItemContentId,
                            ic => ic.Id,
                            (it, ic) => new { it, ic }
                            ).Join
                            (dbContext.ItemCategory.Where(x => x.ItemSubCategory1.Equals(searchSubCategory1, StringComparison.CurrentCultureIgnoreCase) && x.ItemCategoryName.Equals(searchCategory, StringComparison.CurrentCultureIgnoreCase)),
                            itmC => itmC.ic.ItemCategoryKey,
                            itmCat => itmCat.ItemCategoryKey,
                            (itmC, itmCat) => new { itmC, itmCat }
                            );

                    foreach (var element in itemColle)
                    {
                        lstItems.Add(new Item
                        {
                            Id = element.itmC.it.item.Id,
                            ItemContent = new ItemContent
                            {
                                Id = element.itmC.ic.Id,
                                Title = element.itmC.ic.Title,
                                Author = element.itmC.ic.Author,
                                BigImage = element.itmC.ic.BigImage,
                                SmallImage = element.itmC.ic.SmallImage,
                                CreatedBy = element.itmC.ic.CreatedBy,
                                Content = element.itmC.ic.Content,
                                SortDescription = element.itmC.ic.SortDescription,
                                Price = element.itmC.ic.Price,
                                FileName = element.itmC.ic.FileName

                            },
                            Category = element.itmC.it.category
                        });
                    }
                }
            }
            return lstItems;
        }

        public List<Item> GetItemByCategory(int index, int numOfpage, out int numOfRecords, int categoryId, string searchcategory, string searchsubcategory1)
        {
             var lstItem = new List<Item>();

            if (string.IsNullOrEmpty(searchcategory) && string.IsNullOrEmpty(searchsubcategory1))
            {
                lstItem = GetItemByCategory(categoryId);
                
            }

            else
            {
                lstItem = GetItemBysearchcategory(categoryId, searchcategory, searchsubcategory1);
            }
            numOfRecords = lstItem.Count();
            return lstItem.Skip((index - 1) * numOfpage).Take(numOfpage).ToList();
        }

        public Item GetById(int id)
        {
            Item objItem;
            using (var dbContext = new NotesZoneDBContext())
            {
                var v1 = dbContext.Items.Where(i => i.Id == id).Join
                    (dbContext.ItemContents.Where( ic=>ic.IsActive && !ic.IsDeleted),
                item => item.ItemContentId,
                itemcontent => itemcontent.Id,
                    //.Select(c => new Item { Id = c.item.Id, ItemContent = c.itemcontent }).First();
                (item, itemcontent) => new { item, itemcontent }).Join(dbContext.Categories,
                ic => ic.item.Category.Id,
                c => c.Id, (ic, c) => new { ic, c }
                );
// New Imp


                //objItem = new Item { Id = v1.SingleOrDefault().item.Id, ItemContent = v1.SingleOrDefault().itemcontent };

                objItem = new Item { Id = v1.SingleOrDefault().ic.item.Id, ItemContent = v1.SingleOrDefault().ic.itemcontent, Category = v1.SingleOrDefault().c };


            }
            return objItem;

        }

        public List<ItemContent> GetItemContents()
        {
            using( var dbContext = new NotesZoneDBContext())
            {
                return dbContext.ItemContents.Where(ic=>ic.IsActive && !ic.IsDeleted).ToList<ItemContent>();
            }
        }

        public ItemContent GetItemContents(Guid itemCategoryKey)
        {
            using(var dbContext = new NotesZoneDBContext())
            {
                return dbContext.ItemContents.Where(x => x.ItemCategoryKey == itemCategoryKey).FirstOrDefault();
            }
        }

        List<ItemContent> GetItemContents(string itemCategory, string itemSubCategory, string aurthor)
        {
            using(var dbContext = new NotesZoneDBContext())
            {
                return dbContext.ItemCategory.Where(x => x.ItemCategoryName.Equals(itemCategory, StringComparison.CurrentCultureIgnoreCase) &&
                                                                       x.ItemSubCategory1.Equals(itemSubCategory, StringComparison.CurrentCultureIgnoreCase)).Join
                                                                       (dbContext.ItemContents.Where(ic => ic.IsActive && !ic.IsDeleted),
                                                                            icat => icat.ItemCategoryKey,
                                                                            icon => icon.ItemCategoryKey,
                                                                            (icat, icon) => new ItemContent { Id = icon.Id, Title = icon.Title }
                                                                       ).ToList();
 
            }
        }

        public List<ItemContent> GetItemContentByAuthor(string author)
        {
            using (var dbContext = new NotesZoneDBContext())
            {
                return dbContext.ItemContents.Where(x => x.Author.Equals(author, StringComparison.CurrentCultureIgnoreCase) && x.IsActive && !x.IsDeleted).ToList();
            }
        }

        public List<ItemContent> GetItemContentByItemCategory(string itemCategory)
        {
            throw new NotImplementedException();
        }

        public List<ItemContent> GetItemContentByItemSubCategory(string itemSubCategory)
        {
            throw new NotImplementedException();
        }

        public List<ItemContent> GetItemContentByItemCategory(string itemCategory, string itemSubCategory)
        {
            throw new NotImplementedException();
        }

        List<ItemContent> IItemRepository.GetItemContents(string itemCategory, string itemSubCategory, string aurthor)
        {
            throw new NotImplementedException();
        }

        public List<ItemCategory> GetItemCategories()
        {
            using(var dbContext = new NotesZoneDBContext())
            {
                return dbContext.ItemCategory.ToList();
            }
        }

        public ItemCategory GetItemCategory()
        {
            using( var dbContext = new NotesZoneDBContext())
            {
                return dbContext.ItemCategory.FirstOrDefault(); ;
            }
        }

        public bool SaveItem(Item item)
        {
            //item.ItemContent.IsActive = false;
            //item.ItemContent.IsDeleted = false;
            //item.ItemContent.IsAnnouncement = false;
            //item.ItemContent.CreatedBy = "Developement user";
            //item.ItemContent.CreatedDate = System.DateTime.Now;
            ItemContent itemContent = item.ItemContent;
            var IsSuccess = false;
            try
            {
                using (var dbContext = new NotesZoneDBContext())
                {
                    dbContext.Set<Item>().Add(item);
                    dbContext.Entry(itemContent).State = EntityState.Added;
                    dbContext.SaveChanges();
                    IsSuccess = true;
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return IsSuccess;
        }


        public bool SaveItemCategory(ItemCategory itemCategory)
        {
           
            var IsSuccess = false;
            try
            {
                using (var dbContext = new NotesZoneDBContext())
                {
                    dbContext.Set<ItemCategory>().Add(itemCategory);
                    dbContext.Entry(itemCategory).State = EntityState.Added;
                    dbContext.SaveChanges();
                    IsSuccess = true;
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return IsSuccess;
        }

        public bool SaveNumberofView(int itemID)
        {
           bool isRecordSave = false;
            //using(var dbContext = new NotesZoneDBContext())
            //{
            //    try
            //    {
            //        if (itemID != null)
            //        {
            //            var editItem = dbContext.Items.FirstOrDefault(i => i.Id == itemID);
            //            var editItemContent = dbContext.ItemContents.FirstOrDefault(i => i.Id == editItem.ItemContentId);
            //            editItemContent.ModifiedDate = System.DateTime.Now;
            //            editItemContent.NumOfView = Convert.ToInt64(editItemContent.NumOfView + 1); // increasing by 1 on each save
            //            dbContext.Set<ItemContent>().Attach(editItemContent);
            //            dbContext.Entry(editItemContent).Property(x => x.NumOfView).IsModified = true;
            //            dbContext.Entry(editItemContent).State = EntityState.Modified;
            //            dbContext.SaveChanges();
            //            isRecordSave = true;
            //        }
            //    }
            //    catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //    {
            //        foreach (var eve in e.EntityValidationErrors)
            //        {
            //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //            foreach (var ve in eve.ValidationErrors)
            //            {
            //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                    ve.PropertyName, ve.ErrorMessage);
            //            }
            //        }
            //    }
            //}
            return isRecordSave;
        }
    }
}
