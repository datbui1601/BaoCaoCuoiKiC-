using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.Dao
{
   public class CategoryDao
    {
        BuiVanDatContext db = null;
        public CategoryDao()
        {
            db = new BuiVanDatContext();
        }
        public long Insert(Category ennity)
        {
            db.Category.Add(ennity);
            db.SaveChanges();
            return ennity.ID;
        }

        public bool Update(Category entity)
        {
            try
            {
                var user = db.Category.Find(entity.ID);
                user.Name = entity.Name;
                user.Description = entity.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Category.OrderBy(x => x.ID);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public Category GetByID(string cate)
        {
            return db.Category.SingleOrDefault(x => x.Name == cate);
        }
        public Category ViewDetail(int id)
        {
            return db.Category.Find(id);
        }
      public List<Category> ListAll()
        {
            return db.Category.ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Category.Find(id);
                db.Category.Remove(user);
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

