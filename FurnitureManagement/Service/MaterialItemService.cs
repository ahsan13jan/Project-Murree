
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class MaterialItemService
    {
        public static void assignMaterialToItem ( int itemId , int materialId , decimal quantity , int rate )
        {
            var instance = Context.sharedInstance;
            var materialItem = new MaterialItem();
            materialItem.ItemId = itemId;
            materialItem.MaterialId = materialId;
            materialItem.Quantity = quantity;
            materialItem.Rate = rate;
            materialItem.IsDeleted = false;
            materialItem.CreatedAt = DateTime.Now;
            instance.MaterialItems.Add(materialItem);
            instance.SaveChanges();
            MaterialService.addQuantity(materialId, quantity * -1 );
        }

        public static List<MaterialItem> getMaterialItemDetails(int itemId)
        {
            var instance = Context.sharedInstance;

            var list = instance.MaterialItems.Where(x => x.ItemId == itemId && !x.IsDeleted).ToList();

            return list;
        }

        public static List<MaterialItem> getMaterialItemFilter( DateTime? from , DateTime? to , int? articleId )
        {
            var instance = Context.sharedInstance;

            var list = instance.MaterialItems.Where(x =>
            ( x.CreatedAt >= from || from == null ) && 
            (x.CreatedAt <= to || to == null) && 
            (x.Item.JobItem.ArticleId == articleId  || articleId == null  )  && 
            !x.IsDeleted).ToList();

            return list;
        }


    }
}
