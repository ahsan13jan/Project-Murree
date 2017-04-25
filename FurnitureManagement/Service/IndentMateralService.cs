
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class IndentMateralService
    {
        public static void addIndentMaterial( Indent indent , List<Material> list )
        {
            foreach (var material in  list)
            {
                var itemToAdd = new IndentMaterial();
                itemToAdd.IsDeleted = false;
                itemToAdd.CreatedAt = DateTime.Now;
                itemToAdd.IndentId = indent.Id;
                itemToAdd.MaterialId = material.Id;
                itemToAdd.Quantity = material.Quantity;

                var con = Context.sharedInstance;
                con.IndentMaterials.Add(itemToAdd);
                con.SaveChanges();

                MaterialService.addQuantity( (int)itemToAdd.MaterialId, (Decimal)itemToAdd.Quantity);
            }
        }
        public static void deleteIndentMaterial(int id)
        {
            var con = Context.sharedInstance;
            con.IndentMaterials.Find(id).IsDeleted = true;
            con.SaveChanges();
        }
        public static List<IndentMaterial> getIndentMaterial()
        {
            return Context.sharedInstance.IndentMaterials.Where(x => x.IsDeleted == false).ToList();
        }
    }
}
