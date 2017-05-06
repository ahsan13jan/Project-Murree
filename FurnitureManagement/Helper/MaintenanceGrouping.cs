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
            static public List<int> combineIds = new List<int>() { 1022, 1023, 1024, 1025, 1026, 1040, 1041, 1012, 1014, 1015, 1016, 1018, 1019, 1020, 1043 };
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
                static public List<int> ids = new List<int>() { 1012, 1014, 1015, 1016 };
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
                static public List<int> ids = new List<int>() { 1018, 1019, 1020, 1043 };
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
            if (articleId == 1003 || articleId == 1)
                return  (decimal)(1.0 / 4.0);

            if (articleId == 1044)
                return 1 / 2;

            if (articleId == 1027)
                return 1 / 3;


            return 1;
        }
    }

}
