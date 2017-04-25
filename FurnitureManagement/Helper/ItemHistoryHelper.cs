using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement
{
    public partial class ItemLocationService
    {
        string locationName;
        string displayJobItem;
        string itemType;

        public string DisplayJobItem
        {
            get { return displayJobItem; }
            set { displayJobItem = value; }
        }
       

        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

    

     }
}
