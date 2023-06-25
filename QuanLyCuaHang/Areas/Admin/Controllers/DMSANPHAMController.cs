using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Areas.Admin.Controllers
{
    public class DMSANPHAMController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        public ActionResult Index()
        {
            return View(db.DMSANPHAMs.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "MaDMSP,TenDMSP")] DMSANPHAM dMSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.DMSANPHAMs.Add(dMSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dMSANPHAM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMSANPHAM dMSANPHAM = db.DMSANPHAMs.Find(id);
            if (dMSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(dMSANPHAM);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaDMSP,TenDMSP")] DMSANPHAM dMSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dMSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dMSANPHAM);
        }

        public ActionResult Delete(int id)
        {
            DMSANPHAM dMSANPHAM = db.DMSANPHAMs.Find(id);
            db.DMSANPHAMs.Remove(dMSANPHAM);
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
