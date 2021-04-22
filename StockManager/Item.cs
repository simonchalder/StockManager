using System;
using System.Collections.Generic;
using System.Text;

namespace StockManager
{
    class Item
    {
        public Item(string id, int cat, int qty, decimal price, string detail, int sale)
        {
            _id = id;
            Cat = cat;
            Qty = qty;
            Price = price;
            Detail = detail;
            Sale = sale;
        }
        public string _id { get; set; }
        public int Cat { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public string Detail { get; set; }
        public int Sale { get; set; }
    }
}
