using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Areas.Admin.Controllers
{
    public class SPController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        // GET: Admin/SANPHAM
        public ActionResult Index()
        {
            var dm = db.DMSANPHAMs.ToList();
            ViewBag.DMSANPHAMs = dm;
            var sANPHAMs = db.SANPHAMs.Include(s => s.DMSANPHAM);
            return View(sANPHAMs.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.MaDMSP = new SelectList(db.DMSANPHAMs, "MaDMSP", "TenDMSP");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,GiaSP,MieuTa,HinhAnhSP,SoLuongTon,MaDMSP,Sanphamnoibat")] SANPHAM sANPHAM, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(anh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/ImagesUpload"), _FileName);
                    anh.SaveAs(_path);
                    sANPHAM.HinhAnhSP = "Content/ImagesUpload/" + _FileName;
                    db.SANPHAMs.Add(sANPHAM);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaDMSP = new SelectList(db.DMSANPHAMs, "MaDMSP", "TenDMSP", sANPHAM.MaDMSP);
            return View(sANPHAM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDMSP = new SelectList(db.DMSANPHAMs, "MaDMSP", "TenDMSP", sANPHAM.MaDMSP);
            return View(sANPHAM);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,GiaSP,MieuTa,HinhAnhSP,SoLuongTon,MaDMSP,Sanphamnoibat")] SANPHAM sANPHAM, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(anh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/ImagesUpload"), _FileName);
                    anh.SaveAs(_path);
                    sANPHAM.HinhAnhSP = "Content/ImagesUpload/" + _FileName;
                    db.Entry(sANPHAM).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MaDMSP = new SelectList(db.DMSANPHAMs, "MaDMSP", "TenDMSP", sANPHAM.MaDMSP);
            return View(sANPHAM);
        }
 
        public ActionResult Delete(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
