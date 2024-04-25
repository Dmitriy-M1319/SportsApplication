using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class OrderController: Controller
{
   private IOrderRepository _repository;
   private Cart _cart;

   public OrderController(IOrderRepository repository, Cart cart)
   {
      _repository = repository;
      _cart = cart;
   }
   public IActionResult Checkout() => View(new Order());

   [HttpPost]
   public IActionResult Checkout(Order order)
   {
      if (!_cart.Lines.Any())
         ModelState.AddModelError("", "Sorry, your cart is empty!");

      if (ModelState.IsValid)
      {
         order.Lines = _cart.Lines.ToArray(); 
         _repository.SaveOrder(order); 
         _cart.Clear(); 
         return RedirectToPage("/Completed", new { orderId = order.OrderID });
      }
      return View();
   }
}