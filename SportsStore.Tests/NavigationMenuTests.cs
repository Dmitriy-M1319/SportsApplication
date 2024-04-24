using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;

namespace SportsStore.Tests;

public class NavigationMenuTests
{
   [Fact]
   public static void CanSelectCategories()
   {
      Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
      mock.Setup(repository => repository.Products).Returns((new Product[]
      {
         new Product { ProductID = 1, Name = "P1", Category = "Apples" },
         new Product { ProductID = 2, Name = "P2", Category = "Apples" },
         new Product { ProductID = 3, Name = "P3", Category = "Plums" },
         new Product { ProductID = 4, Name = "P4", Category = "Oranges" },
      }).AsQueryable());

      NavigationMenu menu = new NavigationMenu(mock.Object);

      string[] results = ((IEnumerable<string>?)(menu.Invoke() as ViewViewComponentResult)?.ViewData?.Model ??
                          Enumerable.Empty<string>()).ToArray();
      
      Assert.True(new [] { "Apples", "Oranges", "Plums" }.SequenceEqual(results));
   }

   [Fact]
   public static void IndicatesSelectedCategories()
   {
      string categoryToSelect = "Apples";
      Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
      mock.Setup(repository => repository.Products).Returns((new Product[]
      {
         new Product { ProductID = 1, Name = "P1", Category = "Apples" },
         new Product { ProductID = 2, Name = "P2", Category = "Oranges" },
      }).AsQueryable());
      
      NavigationMenu target = new NavigationMenu(mock.Object); 
      target.ViewComponentContext = new ViewComponentContext 
         { 
            ViewContext = new ViewContext
            {
               RouteData = new Microsoft.AspNetCore.Routing.RouteData()
            } 
         }; 
      target.RouteData.Values["category"] = categoryToSelect; 
      string? result = (string?)(target.Invoke() as ViewViewComponentResult)?.ViewData?["SelectedCategory"]; 
      Assert.Equal(categoryToSelect, result);
   }
}