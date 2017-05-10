
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

        public static void addSubBlock( int parentId ,  string Name)
        {
            var con = Context.sharedInstance;

            Block block = new Block();
            block.Name = Name;
            block.ParentId = parentId;
            block.CategoryId = con.Blocks.Find(parentId).CategoryId;
            block.CreatedAt = DateTime.Now;
            block.IsDeleted = false;
            con.Blocks.Add(block);
            con.SaveChanges();

        }
        public static List<Block> getBlocks()
        {
            return Context.sharedInstance.Blocks.Where(x => !(bool)x.IsDeleted && x.ParentId == null ).ToList();

           
        }
        public static List<Block> getSubBlocks( int blockId )
        {
            return Context.sharedInstance.Blocks.Where(x => !(bool)x.IsDeleted && x.ParentId == blockId).ToList();
        }


        public static bool UpdateBlock(Block obj)
        {
            var context=Context.sharedInstance;
            Block b = context.Blocks.Find(obj.Id);
            if (b != null)
            {
                b.CategoryId = obj.CategoryId;
                b.Name = obj.Name;
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static Block getSubBlock(Block b)
        {
            return Context.sharedInstance.Blocks.Where(x => x.ParentId == b.Id).FirstOrDefault();
        }

        public static bool UpdateSubBlock(Block subBlock)
        {
            var context = Context.sharedInstance;
            if (subBlock != null)
            {
                Block b = context.Blocks.Find(subBlock.Id);
                b.Name = subBlock.Name;
                b.ParentId = subBlock.ParentId;
                context.SaveChanges();

                return true;
            }
            else
                return false;


        }


    }
}
