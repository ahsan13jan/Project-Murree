
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
            List<MaterialBundle> listRed = new List<MaterialBundle>();

            var bundles = Context.sharedInstance.MaterialBundles.ToList();

            bundles[4].MaterialBundleItems.ToList().ForEach( x=> x.Quantity = x.Quantity* MainainenceHelper.quantityRatio(articleId) );



            

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

            if (NatureOfWork.ReplacementofTops.GroupEight.ids.Contains(articleId))
                listRed.Add(bundles[7]);


            listRed.Add(bundles[8]);

            return listRed;

        }
        public static List<MaterialBundle> MaterialBundles()
        {
            return Context.sharedInstance.MaterialBundles.ToList();
        }
        public static MaterialBundle getMaterialBundleById( int id)
        {
            return Context.sharedInstance.MaterialBundles.Find(id);
        }

        public static bool IsBundleAvailable(int Id , int articleId , decimal Multipler = 1 )
        {
            var articleMultiplier = Context.sharedInstance.Articles.Find(articleId).Multiple;
            var Bundle = Context.sharedInstance.MaterialBundles.Find(Id);
            bool status = true;

            if ( Id ==  5 )
            {
                Bundle.MaterialBundleItems.ToList().ForEach(x=> x.Quantity = x.Quantity * MainainenceHelper.quantityRatio(articleId) * Multipler);
            }

            if ( Id == 7  )
            {
                var state = IsBundleAvailable(5, articleId, (decimal)(3.0/2.0) );
                if (!state)
                    return false;
            }

            if (Id == 8)
            {
                var state = IsBundleAvailable(5, articleId, (decimal)(5.0 / 4.0));
                if (!state)
                    return false;
            }

            if (Bundle.Id <= 3 )
            {
                Bundle.MaterialBundleItems.ToList().ForEach(x =>
                {
                    if (x.Quantity * articleMultiplier > x.Material.Quantity)
                        status = false;
                });
            }
            else
            {
                Bundle.MaterialBundleItems.ToList().ForEach(x =>
                {
                    if (x.Quantity  > x.Material.Quantity)
                        status = false;
                });
            }

            

            return status;
        }




    }
}
