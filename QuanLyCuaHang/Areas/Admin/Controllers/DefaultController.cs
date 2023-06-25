using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        // GET: Admin/Default
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string tendn, string matkhau)
        {
            var ten = tendn;
            var mk = matkhau;
            var tk = db.QUANTRIs.SingleOrDefault(m=>m.Ten==ten&&m.MatKhau==matkhau);
            if (tk != null)
            {
                Session["admin"] = tk.Ten;
                return RedirectToAction("Index", "HomeAdmin");
            }
            else 
            {
                ModelState.AddModelError("", "Đăng nhập không đúng.");
                return View();
            }           
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Default");
        }
    }
}