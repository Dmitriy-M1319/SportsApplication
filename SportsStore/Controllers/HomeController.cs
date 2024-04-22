using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
    public IStoreRepository repository;

    public HomeController(IStoreRepository repo)
    {
        repository = repo;
    }
    public IActionResult Index() => View(repository.Products);
}
