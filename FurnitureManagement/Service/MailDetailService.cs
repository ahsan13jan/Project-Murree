using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
     static class MailDetailService
    {
         public static void addMailDetail(MailDetail mailDetail)
         {
             var con = Context.sharedInstance;
             con.MailDetails.Add(mailDetail);
             con.SaveChanges();
         }
         public static List<MailDetail> getAllMailDetails()
         {
             return Context.sharedInstance.MailDetails.ToList();
         }

         public static List<MailDetail> getAllIncomingwithNoReplied()
         {
             List<MailDetail> list = Context.sharedInstance.MailDetails.ToList();
             list = list.Where(x => x.MailType == 1 && x.RepliedEmailId == null).ToList();
             return list;
         }
    }
}
