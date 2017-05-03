using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement
{
    public partial class Item
    {
        //string locationDescription;
        //string date;

        //public string Date
        //{
        //    get { return date; }
        //    set { date = value; }
        //}

        //public string LocationDescription
        //{
        //    get { return locationDescription; }
        //    set { locationDescription = value; }
        //}

        public string Display
        {
            get
            {
                return Id + "-" + JobItem.Article.Article_DESC;
            }
        }

    }

    public partial class JobItem
    {


        public string Display
        {
            get
            {
                return Article.Article_DESC + "-" +  Quantity;
            }
        }

    }
}
