using ModelEF.Dao;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var user = new CategoryDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                long id = dao.Insert(cate);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Category thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var result = dao.Update(cate);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập User thành công");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

    }
}