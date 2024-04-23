using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void CanUseRepository()
    {
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        _ = mock.Setup(m => m.Products).Returns((new Product[]
        {
            new() {
                ProductID = 1,
                Name = "P1"
            },
            new() {
                ProductID = 2,
                Name = "P2"
            }
        }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);
        ProductListViewModel result = controller.Index(null)?.ViewData.Model as ProductListViewModel ?? new();
        Product[] prodArray = result.Products.ToArray();
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }

    [Fact]
    public void CanPaginate()
    {
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[]
                {
            new() {
                ProductID = 1,
                Name = "P1"
            },
            new() {
                ProductID = 2,
                Name = "P2"
            },
            new() {
                ProductID = 3,
                Name = "P3"
            },
            new() {
                ProductID = 4,
                Name = "P4"
            },
            new() {
                ProductID = 5,
                Name = "P5"
            },
                }).AsQueryable<Product>());
        HomeController controller = new HomeController(mock.Object);
        controller.PageSize = 3;

        ProductListViewModel result = controller.Index(null, 2)?.ViewData.Model as ProductListViewModel ?? new();
        Product[] prodArray = result.Products.ToArray();
        Assert.Equal(2, prodArray.Length);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);

    }
}