//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LotterySyndicate.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Transaction
    {

        public int TransactionID { get; set; }
        public string UserEmail { get; set; }
        public Nullable<int> NumberOfTickets { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; } = DateTime.Now;

        public Nullable<System.DateTime> BuyDate { get; set; }

        public Transaction()
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
            this.BuyDate = nextFriday;
        }

    }
}
