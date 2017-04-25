
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class ItemService
    {

        public static bool assignItem( int locationId ,int itemId)
        {
            var con = Context.sharedInstance;
            con.Items.Find(itemId).LocationID = locationId;
            ItemLocationService.addHistory(itemId, locationId);
            con.SaveChanges();
            return true;

        }
        public static void unAssignItem(int itemId )
        {
            var con = Context.sharedInstance;
            con.Items.Find(itemId).LocationID = null;
            con.SaveChanges();
        }
        public static void addItem(Item item)
        {
            var con = Context.sharedInstance;
            item.IsDeleted = false;
            item.CreatedAt = DateTime.Now;
            con.Items.Add(item);
            con.SaveChanges();
        }
        public static void deleteItem(int id)
        {
            var con = Context.sharedInstance;
            con.Items.Find(id).IsDeleted = true;
            con.SaveChanges();
        }
        public static List<Item> getItems()
        {
            return  Context.sharedInstance.Items.Where(x => !x.IsDeleted).ToList();

        }
        public static List<Item> getAssignedItems()
        {
            return Context.sharedInstance.Items.Where(x => !x.IsDeleted && x.LocationID != null).ToList();
            
        }
        public static List<Item> getUnAssignedItems()
        {
            return  Context.sharedInstance.Items.Where(x => !x.IsDeleted && x.LocationID == null).ToList();
            //var listModel = new List<ItemModel>();
            //list.ForEach(x =>
            //{
            //    var toAdd = ItemToItemModel(x);
            //    listModel.Add(toAdd);
            //});
            //return listModel;
        }
       
        public static Item getItemById(int id )
        {
            return  Context.sharedInstance.Items.Find(id) ;
        }

        public static bool isAssigned(int id)
        {
            return Context.sharedInstance.Items.Find(id).LocationID == null;
        }

        public static List<Item> getItemsByLocation(int locationId)
        {
            int? locId = locationId;

            if (locId == 0)
                locId = null;

            return Context.sharedInstance.Items.Where(x => x.LocationID == locId && x.IsDeleted == false ).ToList();
        }
        public static List<Item> getItemsByJobItemId(int jobItemId)
        {
            var a = Context.sharedInstance.Items.Where(x => x.JobItemId == jobItemId).ToList();
            return a;
        }

        public static List<Item> filterItems( int? jobId , int? articleId , int? locationId)
        {
            if (locationId == 0)
                locationId = null;
            else if (locationId == null)
                locationId = 0;

            var list = Context.sharedInstance.Items.Where(x=> 
            !x.IsDeleted && 
            (x.JobItem.JobId == jobId  || jobId == null ) && 
            (x.LocationID == locationId || locationId == 0 ) &&
            (x.JobItem.ArticleId == articleId || articleId == null )

            ).ToList();

            list.ForEach(x=> {
                if (x.Location == null)
                    x.Location = new Location() { Name = "Warehouse" };
            });

            return list.OrderByDescending(x => x.CreatedAt).ToList(); ;
        }

        


    }
}
