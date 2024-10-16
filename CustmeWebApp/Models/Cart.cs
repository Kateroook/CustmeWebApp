using CustmeWebApp.Data;
using CustmeWebApp.Data.Migrations;
using Microsoft.EntityFrameworkCore;
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

        public ICollection<CartItem> GetAllCartItems()
        {
            if (CartItems == null)
            {
                CartItems = _context.CartItems
                    .Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Project)
                    .ToList();
            }
            
            return CartItems;
        }
        public decimal? GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => ci.Project.Price * ci.Quantity)
                .Sum();
        }
        public CartItem GetCartItem(Project project)
        {
            return _context.CartItems.SingleOrDefault(ci =>
            ci.Project.Id == project.Id && ci.CartId == Id);
        }

        public void AddToCart(Project project, int quantity)
        {
            var cartItem = GetCartItem(project);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Project = project,
                    Quantity = quantity,
                    CartId = Id
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
        }
    }
}
