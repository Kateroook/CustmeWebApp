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

            return RedirectToAction("Index", "Shop");
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public IActionResult DecreaseQuantity (int id)
        {
            var selectedProject = GetProjectById(id);

            if(selectedProject != null)
            {
                _cart.DecreaseQuantity(selectedProject);
            }

            return RedirectToAction("Index");
        }        
        public IActionResult IncreaseQuantity (int id)
        {
            var selectedProject = GetProjectById(id);

            if(selectedProject != null)
            {
                _cart.IncreaseQuantity(selectedProject);
            }

            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart (int id)
        {
            var selectedProject = GetProjectById(id);

            if(selectedProject != null)
            {
                _cart.RemoveFromCart(selectedProject);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }


        
    }
}
