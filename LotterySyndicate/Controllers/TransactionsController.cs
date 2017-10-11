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

        // GET: Transactions
        public ActionResult Index()
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


        public ActionResult GetTotals()
        {

            //string query = "select sum(NumberOfTickets), sum(Amount) from dbo.Transactions group by UserEmail";
            //var data = db.Transactions.SqlQuery(query);

            var ticketsPerUser = from transaction in db.Transactions
                                 group transaction by transaction.UserEmail
                                 into individual
                                 select new
                                 {
                                     User = individual.Key,
                                     TicketCount = individual.Sum(x => x.NumberOfTickets),
                                     AmountCount = individual.Sum(x => x.Amount),
                                 };

            return View(ticketsPerUser);
            //return PartialView(data.ToList());
        }



        // model for total transactions, can create a new TotalTransactions in the actionresult and fill it with the data and then return the model instead of the data 
        //public class TotalsTransactions
        //{
        //    public string query { get; set; }
        //    public string data { get; set; }
        //}


        //GET: Get all transactions and sort
        //public ActionResult ListAndSort()
        //{
        //    string query = "SELECT * FROM Transactions";
        //    var data = db.Transactions.SqlQuery(query);

        //    return PartialView(data.ToList());

        //}

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
