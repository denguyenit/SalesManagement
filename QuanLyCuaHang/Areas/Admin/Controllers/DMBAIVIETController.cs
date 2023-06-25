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
    public class DMBAIVIETController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Default");
            }
            return View(db.DMBAIVIETs.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "MaDMBV,TenDMBV")] DMBAIVIET dMBAIVIET)
        {
            if (ModelState.IsValid)
            {
                db.DMBAIVIETs.Add(dMBAIVIET);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dMBAIVIET);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DMBAIVIET dMBAIVIET = db.DMBAIVIETs.Find(id);
            if (dMBAIVIET == null)
            {
                return HttpNotFound();
            }
            return View(dMBAIVIET);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaDMBV,TenDMBV")] DMBAIVIET dMBAIVIET)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dMBAIVIET).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dMBAIVIET);
        }

        public ActionResult Delete(int id)
        {
            DMBAIVIET dMBAIVIET = db.DMBAIVIETs.Find(id);
            db.DMBAIVIETs.Remove(dMBAIVIET);
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
