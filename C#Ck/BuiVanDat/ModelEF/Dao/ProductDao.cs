using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.Dao
{
    public class ProductDao
    {
        BuiVanDatContext db = null;
        public ProductDao()
        {
            db = new BuiVanDatContext();
        }
        public long Insert(Product ennity)
        {
            db.Product.Add(ennity);
            db.SaveChanges();
            return ennity.ID;
        }

        public bool Update(Product entity)
        {
            try
            {
                var user = db.Product.Find(entity.ID);
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
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Product.OrderByDescending(x => x.UnitCost);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Quantity).ToPagedList(page, pageSize);
        }
        public Product GetByID(string pro)
        {
            return db.Product.SingleOrDefault(x => x.Name == pro);
        }
        public Product ViewDetail(long id)
        {
            return db.Product.Find(id);
        }
        public Product Find(long id)
        {
            return db.Product.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Product.Find(id);
                db.Product.Remove(user);
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

