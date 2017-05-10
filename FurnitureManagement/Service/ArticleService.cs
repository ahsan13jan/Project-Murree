
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{ 
    static class ArticleService
    {
        public static List<Article> getArticlesByCategory( int catId )
        {
            return Context.sharedInstance.Articles.Where(x => x.Category1.Category_Id == catId).ToList();
        }
        public static List<Article> getArticles()
        {
            return Context.sharedInstance.Articles.ToList();
        }

        public static int getArticleCount( int articleId )
        {
            var article = Context.sharedInstance.Articles.Find(articleId);
            return article.JobItems.Sum(x => x.Quantity).Value ;
        }

    }
}
