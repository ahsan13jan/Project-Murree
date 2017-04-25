using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class JobItemService
    {

        public static void addJobItem( JobItem jobItem )
        {
            var context = Context.sharedInstance;
            jobItem.IsDeleted = false;
            jobItem.CreatedAt = DateTime.Now;
            context.JobItems.Add(jobItem);
            context.SaveChanges();

            for (int i = 0; i< jobItem.Quantity ; i++)
            {
                ItemService.addItem(new Item() { JobItemId = jobItem.Id, UIN = "99"});
            }

        }


        public static List<JobItem> getJobItems(int jobId)
        {
            return Context.sharedInstance.JobItems.Where(x => x.JobId == jobId).ToList();
        }


    }
}
