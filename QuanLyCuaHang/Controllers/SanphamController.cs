using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        private qlbanhangEntities1 db = new qlbanhangEntities1();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Chitietsanpham(int id)
        {
            var dmsp = db.DMSANPHAMs.ToList();
            ViewBag.Danhmucs = dmsp;
            SANPHAM sp = db.SANPHAMs.Where(m => m.MaSP == id).FirstOrDefault();
            ViewBag.Sanpham = sp;
            return View();
        }
        public ActionResult Danhmucsanpham(int id)
        {
            DMSANPHAM dmsp = db.DMSANPHAMs.Where(m => m.MaDMSP == id).FirstOrDefault();
            ViewBag.Danhmuc = dmsp;
            List<SANPHAM> dssp = db.SANPHAMs.Where(m => m.MaDMSP == id).ToList();
            ViewBag.Sanphams = dssp;
            return View();
        }
    }
}