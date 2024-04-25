using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components;

public class CartSummary: ViewComponent
{
    private Cart _cart;

    public CartSummary(Cart cartService)
    {
        _cart = cartService;
    }

    public IViewComponentResult Invoke()
    {
        return View(_cart);
    }
}