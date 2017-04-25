using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement
{
    public partial class MaterialItem
    {
        public double TotalCost
        {
            get
            {
                return (double)Quantity * (double)Rate;
            }
        } 
    }
}
