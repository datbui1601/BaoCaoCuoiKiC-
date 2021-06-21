using ModelEF.Dao;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        public ActionResult Edit(int id)
        {
            SetViewBag();
            var user = new ProductDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.Insert(pro);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Product thành công");
                }
            }
            return View("Index");
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.ProductType = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(pro);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập Product thành công");
                }
            }
            SetViewBag(pro.ProductType);
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Detail(long id)
        {
            var pro = new ProductDao().ViewDetail(id);
            return View(pro);
        }
    }
}