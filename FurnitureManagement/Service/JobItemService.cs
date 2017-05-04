using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class JobItemService
    {

        public static JobItem addJobItem( JobItem jobItem )
        {

            var latestUIN = ArticleService.getArticleCount((int)jobItem.ArticleId);


            var context = Context.sharedInstance;
            jobItem.IsDeleted = false;
            jobItem.CreatedAt = DateTime.Now;
            context.JobItems.Add(jobItem);
            context.SaveChanges();

            jobItem = Context.sharedInstance.JobItems.Find(jobItem.Id);

            

            for (int i = 0; i< jobItem.Quantity ; i++)
            {
                ItemService.addItem(new Item() { JobItemId = jobItem.Id, UIN = jobItem.Article.Prefix + "-" + (latestUIN + 1 + i ).ToString() });
            }

            return jobItem;

        }

        public static List<JobItem> addJobItems( List<JobItem> jobItems )
        {

            List<JobItem> ret = new List<JobItem>();
            jobItems.ForEach(x => {
                ret.Add( addJobItem(x) );
                }
            );

            return ret;
        }


        public static List<JobItem> getJobItems(int jobId)
        {
            return Context.sharedInstance.JobItems.Where(x => x.JobId == jobId).ToList();
        }


    }
}
