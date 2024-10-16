using CustmeWebApp.Data;
using CustmeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace CustmeWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;

            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedProject = GetProjectById(id);

            if (selectedProject != null)
            {
                _cart.AddToCart(selectedProject, 1);
            }

            return RedirectToAction("Index");
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }
    }
}
