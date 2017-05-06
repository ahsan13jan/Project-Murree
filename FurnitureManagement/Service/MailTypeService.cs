using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
   static  class MailTypeService
    {
        public static List<MailType> getMailTypes()
       {
           var context = Context.sharedInstance;
           return context.MailTypes.ToList();
       }

      

    }
}
