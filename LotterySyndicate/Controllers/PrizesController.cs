using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LotterySyndicate.Models;

namespace LotterySyndicate.Controllers
{
    public class PrizesController : Controller
    {
        private LotterySyndicateEntities db = new LotterySyndicateEntities();

        // GET: Prizes
        public ActionResult Index()
        {
            return View(db.Prizes.ToList());
        }

        // GET: Prizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // GET: Prizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Value,WinningNumber")] Prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Prizes.Add(prize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prize);
        }

        // GET: Prizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: Prizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Value,WinningNumber")] Prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prize);
        }

        // GET: Prizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        // POST: Prizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prize prize = db.Prizes.Find(id);
            db.Prizes.Remove(prize);
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
