using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Services;
using Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class ShopListController : Controller
    {
   
        private readonly IShopListRepository _shopListRepository;
        private readonly IListItemService _listItemService;

        public ShopListController(IShopListRepository shopListRepository, IListItemService listItemService)
        {
            _shopListRepository = shopListRepository;
            _listItemService = listItemService;
        }

        public IActionResult Index()
        {
            var shopLists = _shopListRepository.GetAllShopListsWithRecipes();

            var shopListViewModels = shopLists.Select(r => new ShopListViewModel
            {
                ShopListId = r.ShopListId,
                Name = r.Name,
                CreatedDate = r.CreatedDate,
                ListItems = r.ListItems
            });

            return View(shopListViewModels);
        }
        
        public IActionResult Details(int id)
        {
            var shopList = _shopListRepository.GetShopListWithRecipes(id);

            ShopListViewModel shopListViewModel = new ShopListViewModel
            {
                ShopListId = shopList.ShopListId,
                Name = shopList.Name,
                ListItems = shopList.ListItems
            };

            return View(shopListViewModel);

        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(int recipeId, int shopListId)
        {

            _listItemService.RemoveRecipeFromShopList(recipeId, shopListId);

            var shopList = _shopListRepository.GetShopListWithRecipes(shopListId);

            ShopListViewModel shopListViewModel = new ShopListViewModel
            {
                ShopListId = shopList.ShopListId,
                Name = shopList.Name,
                ListItems = shopList.ListItems
            };

            return View(shopListViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ShopListViewModel shopListViewModel)
        {
            if (ModelState.IsValid)
            {
                ShopList shopList = new ShopList
                {
                    ShopListId = shopListViewModel.ShopListId,
                    Name = shopListViewModel.Name,
                    CreatedDate = DateTime.Now,
                };

                _shopListRepository.Add(shopList);

                return RedirectToAction("Index");
            }

            return View(shopListViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var shopList = _shopListRepository.GetById<ShopList>(id);

            _shopListRepository.Delete(shopList);

            return RedirectToAction("Index");

        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var shopList = _shopListRepository.GetById<ShopList>(id);

            ShopListViewModel shopListViewModel = new ShopListViewModel
            {
                ShopListId = shopList.ShopListId,
                Name = shopList.Name,
            };

            return View(shopListViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShopListViewModel shopListViewModel, int id)
        {
            var shopList = _shopListRepository.GetById<ShopList>(id);

            shopList.Name = shopListViewModel.Name;

            _shopListRepository.Update(shopList);
            
            return RedirectToAction("Details", new { id = shopList.ShopListId });

        }
        

    }
}
