using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Controllers
{
    public class KHController : Controller
    {
        // GET: Khachhang
        private qlbanhangEntities1 db = new qlbanhangEntities1();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(string EmailDN, string Matkhau)
        {
            if (ModelState.IsValid)
            {
                var kqdn = DangnhapKH(EmailDN, Matkhau);
                if (kqdn)
                {
                    var kh = GetKHACHHANG(EmailDN);
                    var ten = kh.TenKH;
                    var email = kh.Email;
                    var id = kh.MaKH;
                    Session.Add("EmailKH", email);
                    Session.Add("IdKH", id);
                    Session.Add("TenKH", ten);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }

            }
            return View();
        }

        public KHACHHANG GetKHACHHANG(string email)
        {
            return db.KHACHHANGs.SingleOrDefault(m => m.Email == email);
        }

        public bool DangnhapKH(string email, string matkhau)
        {
            KHACHHANG kh = db.KHACHHANGs.Where(m => m.Email == email && m.MatKhau == matkhau).SingleOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public ActionResult Dangxuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(string tenkh, string email, string matkhau, string nhaplaimk)
        {
            if (matkhau != nhaplaimk)
            {
                ModelState.AddModelError("", "Mật khẩu nhập lại không khớp.");
                return View();
            }
            else
            {
                    var tk = new KHACHHANG();
                    tk.TenKH = tenkh;
                    tk.Email = email;
                    tk.MatKhau = matkhau;
                    db.KHACHHANGs.Add(tk);
                    db.SaveChanges();
                    ModelState.AddModelError("", "Đăng kí thành công");
                    return View();
            }
        }
    }
}