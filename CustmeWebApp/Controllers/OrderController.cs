﻿using CustmeWebApp.Data;
using CustmeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustmeWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;
        //private readonly HttpContext _httpContext;

        public OrderController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Ваш кошик порожній, додайте якусь роботу ^ ^");
            }

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                order.UserId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                //order.UserId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;

                order.OrderDate = DateTime.Now;
                
                var cartItems = _cart.CartItems;
                foreach (var item in cartItems)
                {
                    var orderItem = new OrderItem()
                    {                    
                        Quantity = item.Quantity,
                        ProjectId = item.Project.Id,
                        OrderId = order.Id,
                        Price = Convert.ToInt32(item.Project.Price) * item.Quantity
                    };
                
                order.OrderItems.Add(orderItem);
                order.TotalPrice += orderItem.Price;
                }   
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                HttpContext.Response.Redirect("/Identity/Account/Login");
            }           
        }
    }
}
