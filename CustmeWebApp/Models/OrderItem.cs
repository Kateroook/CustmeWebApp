namespace CustmeWebApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public int ProjectId { get; set; }
        public required Order Order { get; set; }
        public required Project Project { get; set; }
    }
}
