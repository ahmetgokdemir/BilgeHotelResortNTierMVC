using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.Models.BookingTools
{
    public class CartItem   // Room <==> CartItem
    {
        public int ID { get; set; }
        public string RoomTypeName { get; set; }
        public short Amount { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public decimal SubTotal
        {
            get
            {

                return Price * Amount;
            }
        }

        public CartItem()
        {
            Amount++;
        }

    }
}