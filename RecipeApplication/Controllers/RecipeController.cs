using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Services;
using Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;

namespace Web.Controllers
{
    
    public class RecipeController : Controller
    {
        //private readonly IRepository _recipeRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRepository _repository;
        private readonly IListItemService _listItemService;
        private readonly UserManager<RecipeUser> _userManager;

        public RecipeController(IRecipeRepository recipeRepository, 
                                IRepository repository,
                                IListItemService listItemService,
                                UserManager<RecipeUser> userManager)
        {
            _recipeRepository = recipeRepository;
            _repository = repository;
            _listItemService = listItemService;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index(string searchString, string categoryName)
        {

            var categories = _repository.ListAll<Category>().OrderBy(c => c.Name).Select(c => c.Name);

            ViewBag.Categories = categories;
            
            var recipes = _recipeRepository.GetAllRecipesWithCategoryAndShopList();


            if (!String.IsNullOrEmpty(categoryName))
            {
                
                recipes = recipes.Where(r => r.Category.Name == categoryName);

            }
            
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();

                recipes = recipes.Where(r => r.Name.ToUpper().Contains(searchString));
            }

            var recipeViewModels = recipes
                .Select(r => new RecipeViewModel()
                {
                    RecipeId = r.RecipeId,
                    Name = r.Name,
                    PictureId = r.PictureId,
                    Description = r.Description,
                    Price = r.Price,
                    RecipeCategory = r.Category
                });

            return View(recipeViewModels);
        }

        
        public IActionResult Details(int id)
        {
            var recipe = _recipeRepository.GetRecipeWithCategoryAndShopList(id);

            if(recipe == null)
            {
                return NotFound();
            }

            var recipeViewModel = new RecipeViewModel
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                PictureId = recipe.PictureId,
                Description = recipe.Description,
                Price = recipe.Price,
                RecipeCategory = recipe.Category
            };

            return View(recipeViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            
            PopulateCategoriesList();
            return View();
            
        }

        [HttpPost]
        public IActionResult Create(RecipeViewModel recipeViewModel)
        {

            if (ModelState.IsValid)
            {
                Recipe recipe = new Recipe
            {
                Name = recipeViewModel.Name,
                PictureId = recipeViewModel.PictureId,
                Description = recipeViewModel.Description,
                Price = recipeViewModel.Price,
                CategoryId = recipeViewModel.RecipeCategory.CategoryId
                
            };
                _recipeRepository.Add(recipe);
                
                return RedirectToAction("Index");
            }

            return View(recipeViewModel);
                       
        }

        [Authorize]
        // GET: Instructors/Edit/5
        public IActionResult Edit(int id)
        {
            
            var recipe = _recipeRepository.GetById<Recipe>(id);

            if (recipe == null)
            {
                return NotFound();
            }

            RecipeViewModel recipeViewModel = new RecipeViewModel
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                PictureId = recipe.PictureId,
                Description = recipe.Description,
                Price = recipe.Price,
                RecipeCategory = recipe.Category
            };

            PopulateCategoriesList(recipe.RecipeId);
           
            return View(recipeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RecipeViewModel recipeViewModel, int id)
        {
            
            var recipeToUpdate = _recipeRepository.GetById<Recipe>(id);

            if(recipeToUpdate == null)
            {
                return NotFound();
            }

            recipeToUpdate.Name = recipeViewModel.Name;
            recipeToUpdate.PictureId = recipeViewModel.PictureId;
            recipeToUpdate.Description = recipeViewModel.Description;
            recipeToUpdate.Price = recipeViewModel.Price;
            recipeToUpdate.CategoryId = recipeViewModel.RecipeCategory.CategoryId;

            _recipeRepository.Update(recipeToUpdate);

            return RedirectToAction("Details", new { id = recipeToUpdate.RecipeId });
           
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var recipe = _recipeRepository.GetById<Recipe>(id);

            _recipeRepository.Delete(recipe);
            
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddRecipeToList(int id)
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel
            {
                RecipeId = id
            };


            PopulateShopList();

            return View(recipeViewModel);
        }

        [HttpPost]
        public IActionResult AddRecipeToList(RecipeViewModel RecipeViewModel)
        {

            var listItem = _listItemService.AddRecipeToShopList(RecipeViewModel.RecipeId, RecipeViewModel.ShopListId);

            if (listItem == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Receptet finns redan i den valda inköpslistan";

            PopulateShopList();

            return View(RecipeViewModel);
        }
        

        private void PopulateShopList(object selectedShopList = null)
        {

            var shopLists = _repository.ListAll<ShopList>();

            ViewBag.ShopListId = new SelectList(shopLists, "ShopListId", "Name", selectedShopList);
        }


        private void PopulateCategoriesList(object selectedCategory = null)
        {
            var categories = _repository.ListAll<Category>();

            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "Name", selectedCategory);
        }


    }
}
