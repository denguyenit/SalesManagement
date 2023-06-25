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
    public class QUANTRIController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        // GET: Admin/QUANTRI
        public ActionResult Index()
        {
            return View(db.QUANTRIs.ToList());
        }

        // GET: Admin/QUANTRI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUANTRI qUANTRI = db.QUANTRIs.Find(id);
            if (qUANTRI == null)
            {
                return HttpNotFound();
            }
            return View(qUANTRI);
        }

        // GET: Admin/QUANTRI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QUANTRI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,Email,MatKhau")] QUANTRI qUANTRI)
        {
            if (ModelState.IsValid)
            {
                db.QUANTRIs.Add(qUANTRI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qUANTRI);
        }

        // GET: Admin/QUANTRI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUANTRI qUANTRI = db.QUANTRIs.Find(id);
            if (qUANTRI == null)
            {
                return HttpNotFound();
            }
            return View(qUANTRI);
        }

        // POST: Admin/QUANTRI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,Email,MatKhau")] QUANTRI qUANTRI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qUANTRI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qUANTRI);
        }

        // GET: Admin/QUANTRI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUANTRI qUANTRI = db.QUANTRIs.Find(id);
            if (qUANTRI == null)
            {
                return HttpNotFound();
            }
            return View(qUANTRI);
        }

        // POST: Admin/QUANTRI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QUANTRI qUANTRI = db.QUANTRIs.Find(id);
            db.QUANTRIs.Remove(qUANTRI);
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
