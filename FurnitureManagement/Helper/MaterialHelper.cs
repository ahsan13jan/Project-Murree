using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement
{
    public partial class Material
    {
        public string Display
        {
            get { return Id + "-" + Name; }
        }
    }
}
