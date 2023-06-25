using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;
namespace QuanLyCuaHang.Controllers
{
    public class GiohangController : Controller
    {
        private const string GHSession = "GHSession";
        private qlbanhangEntities1 db = new qlbanhangEntities1();
        // GET: Giohang
        public ActionResult Giohangcuaban()
        {
            var gh = Session[GHSession];
            var list = new List<Giohang>();
            if (gh!=null)
            {
                list = (List<Giohang>)gh;
            }
            ViewBag.Giohang = list;
            var dssp = db.SANPHAMs.ToList();
            ViewBag.Sanphams = dssp;
            return View();
        }

        [HttpGet]
        public ActionResult Giohang(int id, int soluonggh)
        {
            var giohang = Session[GHSession];
            if (giohang != null)
            {
                var list = (List<Giohang>)giohang;
                if (list.Exists(m => m.MaSP == id))
                {
                    foreach (var i in list)
                    {
                        if (i.MaSP == id)
                        {
                            i.SoluongDat += soluonggh;
                        }
                    }                  
                }
                else
                {
                    var sp = new Giohang();
                    sp.MaSP = id;
                    sp.SoluongDat = soluonggh;
                    list.Add(sp);
                }
                Session[GHSession] = list;
            }
            else
            {
                var sp = new Giohang();
                sp.MaSP = id;
                sp.SoluongDat = soluonggh;
                var listgh = new List<Giohang>();
                listgh.Add(sp);
                Session[GHSession] = listgh;
            }
            return RedirectToAction("Giohangcuaban", "Giohang");
        }
        [HttpGet]
        public ActionResult Capnhat(int id, int soluongsp)
        {
            var giohang = Session[GHSession];
            var list = (List<Giohang>)giohang;
            foreach(var i in list)
            {
                if (i.MaSP == id)
                {
                    i.SoluongDat = soluongsp;
                }
            }
            Session[GHSession] = list;
            return RedirectToAction("Giohangcuaban", "Giohang");
        }
        public ActionResult Xoa(int id)
        {
            var giohang = Session[GHSession];
            var list = (List<Giohang>)giohang;
            var sp = new Giohang();
            foreach (var i in list)
            {
                if (i.MaSP == id)
                {
                    sp = i;
                }
            }
            list.Remove(sp);
            Session[GHSession] = list;
            return RedirectToAction("Giohangcuaban", "Giohang");
        }
        [HttpPost]
        public ActionResult Dathang(string ten, string sdt, string diachi, string email)
        {
            var donhang = new DONHANG();
            donhang.NgayDat = DateTime.Now;
            donhang.MaKH = (int?)Session["IdKH"];
            donhang.TenNguoiDat = ten;
            donhang.SDT = sdt;
            donhang.DiaChi = diachi;
            donhang.Email = email;
            donhang.TinhTrang = "Đơn hàng mới";
            var madh = Themdon(donhang);
            var giohang = (List<Giohang>)Session["GHSession"];
            foreach(var i in giohang)
            {
                var chitiet = new CHITIETDONHANG();
                chitiet.MaDH = madh;
                chitiet.MaSP = i.MaSP;
                chitiet.SoluongDat = i.SoluongDat;
                db.CHITIETDONHANGs.Add(chitiet);
                db.SaveChanges();
            }
            Session["GHSession"] = null;
            return RedirectToAction("Hoanthanhdon","Giohang");
        }   
        
        public int Themdon(DONHANG dh)
        {
            db.DONHANGs.Add(dh);
            db.SaveChanges();
            return dh.MaDH;
        }       

        public ActionResult Hoanthanhdon()
        {
            return View();
        }
    }
}