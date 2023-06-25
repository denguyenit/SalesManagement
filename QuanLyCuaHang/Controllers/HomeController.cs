using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Controllers
{
    public class HomeController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();
        //lấy view người dùng
        public ActionResult Index()
        {
            var dsdmsp = db.DMSANPHAMs.ToList();
            ViewBag.DMSANPHAMs = dsdmsp;            
            var noibat = "Có";
            var dssp2 = db.SANPHAMs.Where(m => m.Sanphamnoibat == noibat).ToList();
            ViewBag.Noibat = dssp2;
            var dssp = db.SANPHAMs.ToList();
            ViewBag.SANPHAMs = dssp;
            return View(dssp);
        }
        public ActionResult Gioithieu()
        {
            return View();
        }

        public ActionResult Lienhe()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            var gh = Session["GHSession"];
            if (gh != null)
            {
                var list = (List<Giohang>)gh;
                var count = 0;
                foreach (var i in list)
                {
                    count += i.SoluongDat;
                }
                ViewBag.TongGH = count;
            }
            else
            {
                ViewBag.TongGH = 0;
            }
            var sps = db.DMSANPHAMs.ToList();
            ViewBag.MenuSP = sps;
            var tts = db.DMBAIVIETs.ToList();
            ViewBag.MenuTT = tts;
            return PartialView("_TopMenu");
        }

        [HttpPost]
        public ActionResult Timkiem(string searchString)
        {
            List<SANPHAM> dssp = db.SANPHAMs.Where(m => m.TenSP.Contains(searchString)).ToList();
            ViewBag.Timkiem = dssp;
            return View();
        }
    }
}