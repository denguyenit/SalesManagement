using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;
namespace QuanLyCuaHang.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Default");
            }

            var countsp = db.SANPHAMs.Count();
            ViewBag.CountSP = countsp;
            var countkh = db.KHACHHANGs.Count();
            ViewBag.CountKH = countkh;
            var countbv = db.BAIVIETs.Count();
            ViewBag.CountBV = countbv;

            var ngay = DateTime.Today;
            var tt = "Đã xử lý";
            double doanhso = 0;
            List<DONHANG> dsdh = db.DONHANGs.Where(m => m.NgayDat >= ngay && m.TinhTrang == tt).ToList();
            foreach(var t in dsdh)
            {
                List<CHITIETDONHANG> chitiet = db.CHITIETDONHANGs.Where(m => m.MaDH == t.MaDH).ToList();
                foreach(var i in chitiet)
                {
                    SANPHAM sp = db.SANPHAMs.First(m => m.MaSP == i.MaSP);
                    doanhso += (double)(sp.GiaSP * i.SoluongDat);                
                }
            }
            ViewBag.Doanhso = doanhso;

            var dhhomnay = db.DONHANGs.Where(m => m.NgayDat >= ngay).Count();
            if (dhhomnay > 0)
            {
                ViewBag.Dhhomnay = dhhomnay;
            }
            else
            {
                ViewBag.Dhhomnay = 0;
            }
           
            var huy = "Hủy đơn";
            var dhhuy = db.DONHANGs.Where(m => m.NgayDat == ngay && m.TinhTrang == huy).Count();
            if (dhhuy > 0)
            {
                ViewBag.Dhhuy = dhhuy;
            }
            else
            {
                ViewBag.Dhhuy = 0;
            }

            var dssp = db.SANPHAMs.ToList();           
            var tonkho = 0;
            foreach(var item in dssp)
            {
                tonkho += (int)(item.SoLuongTon);
            }
            ViewBag.Tonkho = tonkho;

            ViewBag.Dssanpham = dssp;
            return View();
        }
    }
}