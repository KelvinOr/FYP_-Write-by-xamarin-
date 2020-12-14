using System;
namespace FYP_GeeksClub.Form
{
    public class OrderDetail
    {
        public string CustEmail{ get; set; }
        public string ItemTitle { get; set; }
        public double ItemPrice { get; set; }
        public string ItemOwner { get; set; }
        public int CustPhone { get; set; }
        public string ContMethod { get; set; }
        public bool TranIsAccp { get; set; }
        public string Other { get; set; }
        public string Time { get; set; }
        public string ShowTime { get; set; }
    }
}
