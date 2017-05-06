
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using FurnitureManagement.Helper;

namespace FurnitureManagement.Service
{
    static class MaterialBundleService
    {
        public static List<MaterialBundle> MaterialBundles( int articleId )
        {
            List<MaterialBundle> listRed = Context.sharedInstance.MaterialBundles.ToList();

            listRed[4].MaterialBundleItems.ToList().ForEach( x=> x.Quantity = x.Quantity* MainainenceHelper.quantityRatio(articleId) );



            var bundles =  Context.sharedInstance.MaterialBundles.ToList();

            if (NatureOfWork.Upholstery.GroupOne.ids.Contains(articleId))
                listRed.Add(bundles[0]);

            if (NatureOfWork.Upholstery.GroupTwo.ids.Contains(articleId))
                listRed.Add(bundles[1]);

            if (NatureOfWork.Upholstery.GroupThree.ids.Contains(articleId))
                listRed.Add(bundles[2]);

            if (NatureOfWork.Upholstery.GroupFour.ids.Contains(articleId))
                listRed.Add(bundles[3]);

                listRed.Add(bundles[4]);

            if (NatureOfWork.ConversionofCotNawarintoHardBed.GroupSix.ids.Contains(articleId))
                listRed.Add(bundles[5]);

            if (NatureOfWork.ReplacementofTops.GroupSeven.ids.Contains(articleId))
                listRed.Add(bundles[6]);


            return listRed;

        }
        public static List<MaterialBundle> MaterialBundles()
        {
            return Context.sharedInstance.MaterialBundles.ToList();
        }

        public static bool IsBundleAvailable(int Id , int articleId )
        {

            var Bundle = Context.sharedInstance.MaterialBundles.Find(Id);
            bool status = true;

            if ( Id ==  5 )
            {
                Bundle.MaterialBundleItems.ToList().ForEach(x=> x.Quantity = x.Quantity * MainainenceHelper.quantityRatio(articleId) );
            }

            Bundle.MaterialBundleItems.ToList().ForEach(x=> 
            {
                if (x.Quantity > x.Material.Quantity)
                    status = false;
            });

            return status;
        }




    }
}
