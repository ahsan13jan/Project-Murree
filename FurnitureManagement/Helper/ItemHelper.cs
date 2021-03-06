﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement
{
    public partial class Item
    {

        public string Display
        {
            get
            {
                return UIN + "-" + JobItem.Article.Article_DESC;
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

    public partial class MaterialBundle
    {


        public string Display
        {
            get
            {
                return Name;
            }
        }

    }

    public partial class Block
    {


        public string Display
        {
            get
            {
                if (ParentId == null)
                    return Name + " - " + Category.Category_DESC;
                else
                    return Name;
            }
        }

    }
    public partial class Location
    {


        public string Name { get; set; }

        public string Display
        {
            get
            {
                if (Block  != null)
                    return Block.Name + " - " + Block1.Name;
                else
                    return "Warehouse";
            }
        }

    }
}
