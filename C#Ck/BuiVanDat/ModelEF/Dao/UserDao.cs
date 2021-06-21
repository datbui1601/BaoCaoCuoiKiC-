using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace ModelEF.Dao

{
    public class UserDao
    {
        BuiVanDatContext db = null;
        public UserDao()
        {
            db = new BuiVanDatContext();
        }
        public long Insert(UserAccount ennity)
        {
            db.UserAccount.Add(ennity);
            db.SaveChanges();
            return ennity.ID;
        }

        public bool Update(UserAccount entity)
        {
            try
            {
                var user = db.UserAccount.Find(entity.ID);
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = entity.Password;
                }
                user.Username = entity.Username;
                user.Password = entity.Password;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public IEnumerable<UserAccount> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserAccount> model = db.UserAccount.OrderBy(x => x.ID);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) ||x.Status.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public UserAccount GetByID(string userName)
        {
            return db.UserAccount.SingleOrDefault(x => x.Username == userName);
        }
        public UserAccount ViewDetail(int id)
        {
            return db.UserAccount.Find(id);
        }
        public int Login(string Username, string Password)
        {
            var result = db.UserAccount.SingleOrDefault(x => x.Username == Username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == null)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == Password)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.UserAccount.Find(id);
                db.UserAccount.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
