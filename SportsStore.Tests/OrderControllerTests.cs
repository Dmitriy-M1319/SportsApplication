using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class OrderControllerTests
{
    [Fact]
    public void CanNotCheckoutEmptyCart()
    {
        Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
        Cart cart = new Cart();
        Order order = new Order();
        OrderController target = new OrderController(mock.Object, cart);

        ViewResult? result = target.Checkout(order) as ViewResult;
        mock.Verify(r => r.SaveOrder(It.IsAny<Order>()), Times.Never);
        Assert.True(string.IsNullOrEmpty(result?.ViewName));
        Assert.False(result?.ViewData.ModelState.IsValid);
    }

    [Fact]
    public void CanNotCheckoutInvalidShippingDetails()
    {
        Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
        Cart cart = new Cart(); 
        cart.AddItem(new Product(), 1);
        OrderController target = new OrderController(mock.Object, cart);
        target.ModelState.AddModelError("error", "error");
        ViewResult? result = target.Checkout(new Order()) as ViewResult;
        mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
        Assert.True(string.IsNullOrEmpty(result?.ViewName));
        Assert.False(result?.ViewData.ModelState.IsValid);
    }
}