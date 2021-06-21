using ModelEF.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Models;
using TestUngDung.Common;


namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.Username);
                    var userSesion = new UserLogin();
                    userSesion.Username = user.Username;
                    userSesion.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSesion);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu sai");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View("Index");
        }
    }
}
