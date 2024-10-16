using CustmeWebApp.Data;
using CustmeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace CustmeWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly Cart _cart;

        public CartController(Cart cart)
        {
            _cart = cart;
        }
        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;

            return View(_cart);
        }
    }
}
