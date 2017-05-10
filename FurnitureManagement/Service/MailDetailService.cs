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
             list = list.Where(x => x.MailType == 1 && x.RepliedEmailId == null && x.Type=="Reply").ToList();
             List<MailDetail> updatedList = new List<MailDetail>();
             foreach (var item in list)
             {
                 if (isMailReplyExist(item))
                 {
                     updatedList.Add(item);
                 }
             }
                return updatedList;
         }

         private static bool isMailReplyExist(MailDetail m )
         {
             List<MailDetail> list = Context.sharedInstance.MailDetails.ToList();

             foreach (var item in list)
             {
                 if (m.Id == item.RepliedEmailId)
                 {
                     return false;
                 }

             }

             return true;
         }


         public static List<MailDetail> filterMails(int? mailType , DateTime? toDate , DateTime? fromDate, int? type)
         {
             List<MailDetail> list = Context.sharedInstance.MailDetails.ToList();
             if (mailType == 0)
                 list = list.Where(x => x.MailType > 0).ToList();
             else
                 list = list.Where(x => x.MailType == mailType).ToList();

             if (toDate != null)
                 list = list.Where(x => x.Date >= toDate).ToList();
             if (fromDate != null)
                 list = list.Where(x => x.Date <= fromDate).ToList();
             if (type == 1)
             {
                 list=getAllRepliedEmails(list);
             }
                
             else if (type == 2)
             {
                 list = list.Where(x => x.MailType == 1 && x.RepliedEmailId == null && x.Type == "Reply").ToList();
                 List<MailDetail> updatedList = new List<MailDetail>();
                 foreach (var item in list)
                 {
                     if (isMailReplyExist(item))
                     {
                         updatedList.Add(item);
                     }
                 }
                 list = updatedList;
                 
             }
                 

             return list;
                 
         }

         private static List<MailDetail> getAllRepliedEmails(List<MailDetail>list)
         {
             list = list.Where(x => x.RepliedEmailId != null).ToList();
             List<MailDetail> temp = Context.sharedInstance.MailDetails.ToList();
             List<MailDetail> finalList = new List<MailDetail>();

             foreach (var item in list)
             {
                 MailDetail m = temp.Where(x=>x.Id==(int)item.RepliedEmailId).FirstOrDefault();
                 finalList.Add(m);
                 
             }
             return finalList;
         }



        
    }
}
