using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class JobService
    {


        public static Job addJob( Job job )
        {
            var context = Context.sharedInstance;
            job.IsDeleted = false;
            job.CreatedAt = DateTime.Now;
            var added = context.Jobs.Add(job);
            context.SaveChanges();
            return added;
        }
        public static void deleteJob(int id )
        {
            var context = Context.sharedInstance;
            context.Jobs.Find(id).IsDeleted = true;
            context.SaveChanges();
        }


        public static List<Job> getJobs()
        {
            var context = Context.sharedInstance;
            List <Job> listOfJobs=context.Jobs.Where(x => !x.IsDeleted).ToList();
            foreach (Job item in listOfJobs)
            {
                if (item.Category != null)
                {
                    int id = (int)item.Category;
                    item.CATEGORYDESC = JobService.CategoryDescription(id);
                }
            }

            return listOfJobs;
        }

        public static List<Category> getCategories()
        {
            return Context.sharedInstance.Categories.ToList();
        }

        public static  string CategoryDescription(int categoryId)
        {
            var context = Context.sharedInstance;
            var list= context.Categories.Where(x => x.Category_Id == categoryId).ToList();
            return list.FirstOrDefault().Category_DESC;
        }
    }
}
