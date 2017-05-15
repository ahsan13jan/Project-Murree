using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Helper
{


    public static class NatureOfWork
    {
        public static class Upholstery
        {
            static public List<int> combineIds = new List<int>() { 1022, 1023, 1024, 1025, 1026, 1040, 1041, 1012, 1014, 1015, 1016, 1036 , 1018, 1019, 1020, 1043 };
            public static class GroupOne
            {
                static public List<int> ids = new List<int>() { 1022, 1023, 1024, 1025, 1026 };
            }
            public static class GroupTwo
            {
                static public List<int> ids = new List<int>() { 1040, 1041 };
            }
            public static class GroupThree
            {
                static public List<int> ids = new List<int>() { 1012, 1014, 1015, 1016 , 1036 };
            }
            public static class GroupFour
            {
                static public List<int> ids = new List<int>() { 1018, 1019, 1020, 1043 };
            }
        }

        public static class ConversionofCotNawarintoHardBed
        {
            static public List<int> combineIds = new List<int>() { 1039 };

            public static class GroupSix
            {
                static public List<int> ids = new List<int>() { 1039 };
            }

        }

        public static class ReplacementofTops
        {
            static public List<int> combineIds = new List<int>() { 1018, 1019, 1020, 1043, 1027, 1034, 1035, 1038, 1045 };
            public static class GroupSeven
            {
                static public List<int> ids = new List<int>() { 1018, 1019, 1020, 1044 };
            }
            public static class GroupEight
            {
                static public List<int> ids = new List<int>() { 1027, 1034, 1035, 1038, 1045 };
            }

        }
    }


    static public class MainainenceHelper
    {
        static public decimal quantityRatio( int articleId )
        {




            if (
                articleId == 1018 ||
                articleId == 1019 ||
                articleId == 1020 ||
                articleId == 1024 ||
                articleId == 1041
                )
                return  (decimal)(1.0/2.0);

            if (articleId == 1027 ||
                articleId == 1028 )
                return (decimal)(1.0 / 3.0);
          


            if (articleId == 2 ||
                 articleId == 1003 ||
                 articleId == 1006 ||
                 articleId == 1039 ||
                 articleId == 1022 ||
                 articleId == 1023 ||
                 articleId == 1009 ||
                 articleId == 1010

                 )
                return (decimal)(1.0 / 4.0);



            if (articleId == 1040)
                return (decimal)(1.0 / 6.0);

            if (
            articleId == 1029 ||
            articleId == 1030 ||
            articleId == 1031 ||
            articleId == 1032 ||
            articleId == 1033 ||
            articleId == 1034 ||
            articleId == 1035 ||
            articleId == 1037 ||
            articleId == 1038 ||
            articleId == 1044 ||
            articleId == 1045 ||
            articleId == 1046 ||
            articleId == 1047 ||
            articleId == 1025 ||
            articleId == 1026
            )
                return (decimal)(1.0 / 8.0);







            if (articleId == 1014 ||
                articleId == 1015 ||
                articleId == 1016 ||
                articleId == 1042 ||
                articleId == 1043 
                )
                return (decimal)(1.0 / 12.0);


            return 1;
        }
    }

}
