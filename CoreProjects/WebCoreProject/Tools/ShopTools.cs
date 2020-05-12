using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreProject.Models;

namespace WebCoreProject.Tools
{
    public class ShopTools
    {
        private readonly WebCoreProjectContext db = new WebCoreProjectContext();

        public List<Shop> GetAllShop()
        {
            var res = db.Shop.Where(x => true).ToList();
            return res;
        }

        public bool AddShop(Shop shop)
        {
            db.Shop.Add(shop);
            if (db.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }
    }
}
