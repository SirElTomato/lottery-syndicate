using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LotterySyndicate.Models
{
    public class UniqueUsersTransactions
    {
        public string User { get; set; }
        public int TicketCount { get; set; }
        public double AmountCount { get; set; }
    }
}