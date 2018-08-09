using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCake.Const;
using ProjectCake.Data;
using ProjectCake.Data.Migrations;
using ProjectCake.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace ProjectCake.Controllers
{

    public class ProductController : Controller
    {

        private ApplicationDbContext _context;


        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> model = _context.Set<Product>().ToList().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Date = p.Date,
                CategoryId = p.CategoryId,
                ImageProd = p.ImageProd ?? Consts.DefaultImageProd


            });



            return View("Index", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminIndex()
        {
            IEnumerable<ProductViewModel> model = _context.Set<Product>().ToList().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Date = p.Date,
                CategoryId = p.CategoryId,
                ImageProd = p.ImageProd ?? Consts.DefaultImageProd
            });

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddEditProduct(long? id)
        {
            ProductViewModel model = new ProductViewModel();
            if (id.HasValue)
            {
                Product product = _context.Set<Product>().SingleOrDefault(c => c.Id == id.Value);

                if (product != null)
                {
                    model.Id = product.Id;
                    model.Name = product.Name;
                    model.Description = product.Description;
                    model.Price = product.Price;
                    model.Date = product.Date;
                    model.CategoryId = product.CategoryId;
                    model.ImageProd = product.ImageProd;

                }
            }

            var caterogies = _context.Set<Category>().ToList();
            var categoriesViewModel = new List<CategoryViewModel>();
            foreach (var category in caterogies)
                categoriesViewModel.Add(new CategoryViewModel { Id = category.Id, Name = category.Name });
            model.Categories = categoriesViewModel;

            return PartialView("~/Views/Product/_AddEditProduct.cshtml", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddEditProduct(long? id, ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = !id.HasValue;
                    Product product = isNew ? new Product
                    {
                        AddedDate = DateTime.UtcNow
                    } : _context.Set<Product>().SingleOrDefault(s => s.Id == id.Value);

                    product.Id = model.Id;
                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.Date = DateTime.Now;
                    product.ModifiedDate = DateTime.Now;
                    product.CategoryId = model.CategoryId;




                    if (isNew)
                    {
                        _context.Add(product);
                    }
                    _context.SaveChanges();

                    if (model.File != null)
                    {
                        var basePath = $"/images/UploadImg/Product/{product.Id}";
                        var productDirectoryPath = $"wwwroot{basePath}";
                        if (!Directory.Exists(productDirectoryPath))
                            Directory.CreateDirectory(productDirectoryPath);

                        var pathForClient = $"{basePath}/{model.File.FileName}";
                        var pathForDB = $"{productDirectoryPath}/{model.File.FileName}";
                        using (var stream = new FileStream(pathForDB, FileMode.Create))
                        {
                            model.File.CopyTo(stream);
                            product.ImageProd = pathForClient;
                            _context.SaveChanges();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("AdminIndex");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteProduct(long id)
        {
            Product product = _context.Set<Product>().SingleOrDefault(c => c.Id == id);
            string productName = product.Name;
            return PartialView("~/Views/Product/_DeleteProduct.cshtml", model: productName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteProduct(long id, IFormCollection form)
        {
            Product product = _context.Set<Product>().SingleOrDefault(c => c.Id == id);
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("AdminIndex");
        }
    }
}