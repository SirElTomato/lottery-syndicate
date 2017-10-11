using LotterySyndicate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LotterySyndicate.Controllers
{


    public class WebAPIController : ApiController
    {

        private LotterySyndicateEntities db = new LotterySyndicateEntities();

        // GET: Transactions

        public IHttpActionResult Get()
        {

            var ticketsPerUser = from transaction in db.Transactions
                                 group transaction by transaction.UserEmail
                                 into individual
                                 select new
                                 {
                                     User = individual.Key,
                                     TicketCount = individual.Sum(x => x.NumberOfTickets),
                                     AmountCount = individual.Sum(x => x.Amount),
                                 };



            return Ok(ticketsPerUser);

        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}