using System;
using System.Collections.Generic;
using System.Text;

namespace FYP_GeeksClub.Form
{
    public class ShopItemDetail
    {
        public string title { get; set; }
        public string detail { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string imageURL { get; set; }
        public bool isSecondHand { get; set; }
        public bool saleIng { get; set; }
        public string owner { get; set; }
        public string time { get; set; }
    }
}
