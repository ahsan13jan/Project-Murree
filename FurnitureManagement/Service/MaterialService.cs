using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class MaterialService
    {
        public static void addQuantity( int Id , decimal quantity)
        {
            var con = Context.sharedInstance;
            var material = con.Materials.Find(Id);
            material.Quantity += quantity;
            con.SaveChanges();
        }


        public static void updateMaterialRates( List<Material> list )
        {
            var con = Context.sharedInstance;

            list.ForEach(x=> {

                var material = con.Materials.Find(x.Id);
                material.Rate = x.Rate;
                

            });

            con.SaveChanges();
        }


        public static List<Material> getMaterialModels()
        {
            return Context.sharedInstance.Materials.ToList();
            
        }

        public static List<Material> getMaterials()
        {
            return Context.sharedInstance.Materials.ToList();
            
        }


    }
}
