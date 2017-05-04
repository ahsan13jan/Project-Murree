using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Helper
{


    static class NatureOfWork
    {
        static class Upholstery
        {
            static class GroupOne
            {
                static public List<int> ids = new List<int>() {24 , 25 , 26 ,27 , 28 };
            }
            static class GroupTwo
            {
                static public List<int> ids = new List<int>() {42 , 43 };
            }
            static class GroupThree
            {
                static public List<int> ids = new List<int>() { 14, 16 , 17 , 18 , 38 };
            }
            static class GroupFour
            {
                static public List<int> ids = new List<int>() { 14, 16, 17, 18, 38 };
            }
        }
    }

    class Group
    {
        public int No { get; set; }
        public List<int> Ids { get; set; }

    }
}
