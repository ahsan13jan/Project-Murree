
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class BlockService
    {
        public static void addBlock( string Name , int catId )
        {
            var con = Context.sharedInstance;

            Block block = new Block();
            block.Name = Name;
            block.CategoryId = catId;
            block.CreatedAt = DateTime.Now;
            block.IsDeleted = false;
            con.Blocks.Add(block);
            con.SaveChanges();

        }

        public static void addSubBlock( int parentId ,  string Name, int catId)
        {
            var con = Context.sharedInstance;

            Block block = new Block();
            block.Name = Name;
            block.CategoryId = catId;
            block.ParentId = parentId;
            block.CreatedAt = DateTime.Now;
            block.IsDeleted = false;
            con.Blocks.Add(block);
            con.SaveChanges();

        }
        public static List<Block> getBlocks()
        {
            return Context.sharedInstance.Blocks.Where(x => !(bool)x.IsDeleted && x.ParentId == null).ToList();
        }
        public static List<Block> getSubBlocks( int blockId )
        {
            return Context.sharedInstance.Blocks.Where(x => !(bool)x.IsDeleted && x.ParentId == blockId).ToList();
        }


    }
}
