using System.ComponentModel.DataAnnotations;

namespace CustmeWebApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        public Project Project { get; set; }
        public Cart Cart { get; set; }
    }
}
