using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCake.Data;
using ProjectCake.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectCake.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CategoryViewModel> model = _context.Set<Category>().ToList().Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            });

            return View("CategoryIndex", model);
        }

        // GET: Category/Create
        [HttpGet]
        public IActionResult AddEditCategory(long? id)
        {
            CategoryViewModel model = new CategoryViewModel();
            if (id.HasValue)
            {
                Category category = _context.Set<Category>().SingleOrDefault(c => c.Id == id.Value);

                if (category != null)
                {
                    model.Id = category.Id;
                    model.Name = category.Name;
                }
            }

            return PartialView("~/Views/Category/_AddEditCategory.cshtml", model);
        }

        [HttpPost]
        public IActionResult AddEditCategory(long? id, CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = !id.HasValue;
                    Category category = isNew ? new Category
                    {
                    } : _context.Set<Category>().SingleOrDefault(s => s.Id == id.Value);

                    category.Id = model.Id;
                    category.Name = model.Name;

                    if (isNew)
                    {
                        _context.Add(category);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: Category/Delete/5
        public IActionResult DeleteCategory(long id)
        {
            Category category = _context.Set<Category>().SingleOrDefault(c => c.Id == id);
            string categoryName = category.Name;
            return PartialView("~/Views/Category/_DeleteCategory.cshtml", model: categoryName);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public IActionResult DeleteCategory(long id, IFormCollection collection)
        {
            Category category = _context.Set<Category>().SingleOrDefault(c => c.Id == id);
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}