using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components;

public class NavigationMenu: ViewComponent
{
    private IStoreRepository repository;

    public NavigationMenu(IStoreRepository storeRepository)
    {
        repository = storeRepository;
    }
    public IViewComponentResult Invoke()
    {
        return View(repository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
    }
}