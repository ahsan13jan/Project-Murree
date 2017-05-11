
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
            using (var Context = new FurnitureEntities())
            {
                return Context.Articles.Where(x => x.Category1.Category_Id == catId).ToList();
            }
                
        }
        public static List<Article> getArticles()
        {
            using (var Context = new FurnitureEntities())
            {
                return Context.Articles.ToList();
            }
            
        }

        public static int getArticleCount( int articleId )
        {
            using (var Context = new FurnitureEntities())
            {
                var article = Context.Articles.Find(articleId);
                return article.JobItems.Sum(x => x.Quantity).Value;
            }

            
        }

    }
}
