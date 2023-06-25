using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;
namespace QuanLyCuaHang.Controllers
{
    public class TintucController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();
        // GET: Tintuc
        public ActionResult Tintuc(int id)
        {
            DMBAIVIET dmbv = db.DMBAIVIETs.Where(m => m.MaDMBV == id).FirstOrDefault();
            ViewBag.Danhmuc = dmbv;
            List<BAIVIET> dsbv = db.BAIVIETs.Where(m => m.MaDMBV == id).ToList();
            ViewBag.Sanphams = dsbv;
            return View();
        }
    }
}