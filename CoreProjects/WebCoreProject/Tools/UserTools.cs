using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreProject.Models;

namespace WebCoreProject.Tools
{
    public class UserTools
    {
        private readonly WebCoreProjectContext db = new WebCoreProjectContext();

        public List<User> GetAllUser()
        {
            var res = db.User.Where(x => true).ToList();
            return res;
        }

        public bool AddUser(User user)
        {
            db.User.Add(user);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
