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
    public class BAIVIETController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        public ActionResult Index()
        {
            var dmbv = db.DMBAIVIETs.ToList();
            ViewBag.DMBAIVIETs = dmbv;
            var bAIVIETs = db.BAIVIETs.Include(b => b.DMBAIVIET);
            return View(bAIVIETs.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.MaDMBV = new SelectList(db.DMBAIVIETs, "MaDMBV", "TenDMBV");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "MaBV,TenBV,MaDMBV,HinhAnhBV,ChiTietBV")] BAIVIET bAIVIET, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(anh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/ImagesBV"), _FileName);
                    anh.SaveAs(_path);
                    bAIVIET.HinhAnhBV = "Content/ImagesBV/" + _FileName;
                    db.BAIVIETs.Add(bAIVIET);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                
            }
            ViewBag.MaDMBV = new SelectList(db.DMBAIVIETs, "MaDMBV", "TenDMBV", bAIVIET.MaDMBV);
            return View(bAIVIET);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAIVIET bAIVIET = db.BAIVIETs.Find(id);
            if (bAIVIET == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDMBV = new SelectList(db.DMBAIVIETs, "MaDMBV", "TenDMBV", bAIVIET.MaDMBV);
            return View(bAIVIET);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaBV,TenBV,MaDMBV,HinhAnhBV,ChiTietBV")] BAIVIET bAIVIET, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(anh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/ImagesBV"), _FileName);
                    anh.SaveAs(_path);
                    bAIVIET.HinhAnhBV = "Content/ImagesBV/" + _FileName;
                    db.Entry(bAIVIET).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                  
            }
            ViewBag.MaDMBV = new SelectList(db.DMBAIVIETs, "MaDMBV", "TenDMBV", bAIVIET.MaDMBV);
            return View(bAIVIET);
        }

        public ActionResult Delete(int id)
        {
            BAIVIET bAIVIET = db.BAIVIETs.Find(id);
            db.BAIVIETs.Remove(bAIVIET);
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
