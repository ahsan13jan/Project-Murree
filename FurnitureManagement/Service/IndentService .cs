
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class IndentService
    {
        public static void addIndent( Indent indent , List<Material> list )
        {
            using (var Context = new FurnitureEntities())
            {
                indent.IsDeleted = false;
                indent.CreatedAt = DateTime.Now;
                var con = Context;
                con.Indents.Add(indent);
                con.SaveChanges();
                IndentMateralService.addIndentMaterial(indent, list);
            }

            

        }
        public static void deleteIndent(int id)
        {
            var con = Context.sharedInstance;
            con.Indents.Find(id).IsDeleted = true;
            con.SaveChanges();
        }
        public static List<Indent> getIndents()
        {
            return Context.sharedInstance.Indents.Where(x => x.IsDeleted == false).ToList();
        }
    }
}
