using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement
{
     public partial class Job
    {

        private string categorydesc;

        public string CATEGORYDESC
        {
            get { return categorydesc; }
            set { categorydesc = value; }
        }
        public string Display
        {
            get
            {
                return JobNo + " - " + ContractorName;
            }
        }
    }
}
