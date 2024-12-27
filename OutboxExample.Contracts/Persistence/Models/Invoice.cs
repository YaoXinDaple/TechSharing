using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboxExample.Contracts.Persistence.Models
{
    public class Invoice
    {
        private Invoice() { }
        public Invoice(string orderNo, Guid id, string buyerName, List<StandardEntry> entries)
        {
            OrderNo = orderNo;
            Id = id;
            BuyerName = buyerName;
            Entries = entries;
        }

        public string OrderNo { get; set; }

        public Guid Id { get; set; }

        public string BuyerName { get; set; }

        public List<StandardEntry> Entries { get; set; }
    }

    public class StandardEntry
    {
        private StandardEntry() { }
        public StandardEntry(Guid id, int number, string itemName, decimal amount, bool isDiscountLine)
        {
            Id = id;
            Number = number;
            ItemName = itemName;
            Amount = amount;
            IsDiscountLine = isDiscountLine;
        }

        public Guid Id { get; set; }
        public int Number { get; set; }

        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public bool IsDiscountLine { get; set; }
    }
}
