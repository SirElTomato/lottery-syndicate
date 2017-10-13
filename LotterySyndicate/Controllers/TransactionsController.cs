using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LotterySyndicate;
using Newtonsoft.Json;
using LotterySyndicate.Models;

namespace LotterySyndicate.Controllers
{
    public class TransactionsController : Controller
    {
        private LotterySyndicateEntities db = new LotterySyndicateEntities();

        public DateTime GetWeekConfiguration()
        {
            var timestamp = DateTime.Now;
            var daysSinceFriday = DayOfWeek.Friday - timestamp.DayOfWeek;
            DateTime nextFriday = timestamp.AddDays(daysSinceFriday);
            if (timestamp.DayOfWeek == DayOfWeek.Friday)
            {
                nextFriday = nextFriday.Date.AddDays(7);
            }
            else
            {
                nextFriday = nextFriday.Date;
            }

            return nextFriday;
        }

        // GET: Transactions
        public ActionResult Index()
        {
            var query = "SELECT SUM(NumberOfTickets) FROM dbo.transactions";
            var result = db.Transactions.SqlQuery(query);
            ViewBag.CompanyTotalTickets = result;

            var totalAmount = db.Transactions.Select(t => t.Amount ?? 0).Sum();
            var totalTickets = db.Transactions.Select(t => t.NumberOfTickets ?? 0).Sum();
            var firstPrize = db.Prizes.Select(p => p.Value).ToList();

            ViewData["TotalTickets"] = totalTickets;
            ViewBag.TotalAmount = (int)totalAmount;
            ViewBag.FirstPrize = (int)firstPrize[0];
            

            


            return View(db.Transactions.ToList());

        
        }

        public PartialViewResult ShowThisWeek()
        {

            var nextBuyDate = GetWeekConfiguration();
            var lastBuyDate = nextBuyDate.AddDays(-7);
            var result = (from t in db.Transactions
                          where t.BuyDate >= lastBuyDate
                          where t.BuyDate <= nextBuyDate
                          select t);


            return PartialView("~/Views/Transactions/_ShowThisWeek.cshtml", result.ToList());
        }

        public PartialViewResult ShowLastWeek()
        {

            var previousWeekBuyDate = GetWeekConfiguration().AddDays(-7);
            var previousWeeklastBuyDate = previousWeekBuyDate.AddDays(-7);
            var result = (from t in db.Transactions
                          where t.BuyDate >= previousWeeklastBuyDate
                          where t.BuyDate <= previousWeekBuyDate
                          select t).ToList();

            return PartialView("~/Views/Transactions/_ShowLastWeek.cshtml", result);
        }

        public ActionResult ShowAll()
        {

            return View(db.Transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }


        // GET: Transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,UserEmail,NumberOfTickets,Amount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,UserEmail,NumberOfTickets,Amount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
