using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
   static class ItemLocationService
    {
        public static List<ItemLocation> getItemHistoryByItemId(int itemId)
        {
            return  Context.sharedInstance.ItemLocations.Where(x => x.ItemId == itemId).ToList();
        }
        public static void addHistory( int itemId , int locationId )
        {
            ItemLocation toAdd = new ItemLocation();
            toAdd.ItemId = itemId;
            toAdd.LocationId = locationId;
            toAdd.Ceated_At = DateTime.Now;
            var context = Context.sharedInstance;
            context.ItemLocations.Add(toAdd);
            context.SaveChanges();

        }

    }
}
