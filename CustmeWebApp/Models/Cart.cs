using CustmeWebApp.Data;
using CustmeWebApp.Data.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustmeWebApp.Models
{
    public class Cart
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }


        public ICollection<CartItem> CartItems { get; set; }

        private readonly ApplicationDbContext _context;

        public Cart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }
    }
}
