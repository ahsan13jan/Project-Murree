
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FurnitureManagement.Service
{
    static class MaterialBundleService
    {
        public static List<MaterialBundle> MaterialBundles( )
        {
            return Context.sharedInstance.MaterialBundles.ToList();
        }

        public static bool IsBundleAvailable(int Id )
        {

            var Bundle = Context.sharedInstance.MaterialBundles.Find(Id);
            bool status = true;
            Bundle.MaterialBundleItems.ToList().ForEach(x=> 
            {
                if (x.Quantity > x.Material.Quantity)
                    status = false;
            });

            return status;
        }


    }
}
