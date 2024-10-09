﻿namespace CustmeWebApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public int OrderId { get; set; }
        public int BookId { get; set; }
        public required Order Order { get; set; }
        public required Service Service { get; set; }
    }
}
