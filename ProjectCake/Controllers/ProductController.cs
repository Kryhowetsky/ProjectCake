using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCake.Const;
using ProjectCake.Data;
using ProjectCake.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Controllers
{

    public class ProductController : Controller
    {
        
        private ApplicationDbContext _context;

        
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int? category, string name)
        {
            IQueryable<Product> products = _context.Product.Include(x => x.Category);
            if (category != null && category != 0)
            {
                products = products.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }
            
            IndexSortViewModel viewModel = new IndexSortViewModel
            {  
                FilterViewModel = new FilterViewModel(_context.Category.ToList(), category, name),
                
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AdminIndex(int? category, string name, int page=1, SortState sortOrder = SortState.NameAsc)
        {
            //IEnumerable<ProductViewModel> model = _context.Set<Product>().ToList().Select(p => new ProductViewModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    Price = p.Price,
            //    Date = p.Date,
            //    CategoryId = p.CategoryId,
            //    ImageProd = p.ImageProd ?? Consts.DefaultImageProd,
            //    Category = p.Category,
            //    CategoryName = _context.Set<Category>().SingleOrDefault(c => c.Id == p.CategoryId).Name,
            //});

            int pageSize = 6;

            //Filtration
            IQueryable<Product> products = _context.Product.Include(x => x.Category);
            if (category != null && category != 0)
            {
                products = products.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            //Sort
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriceAsc:
                    products = products.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case SortState.CategoryAsc:
                    products = products.OrderBy(s => s.Category.Name);
                    break;
                case SortState.CategoryDesc:
                    products = products.OrderByDescending(s => s.Category.Name);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;

            }

            //Paging
            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexSortViewModel viewModel = new IndexSortViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_context.Category.ToList(), category, name),
                Products = items
            };


            return View(viewModel);
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

        public IActionResult DetailProd(int id)
        {
            ProductViewModel product = _context.Set<Product>().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Date = p.Date,
                CategoryId = p.CategoryId,
                ImageProd = p.ImageProd ?? Consts.DefaultImageProd
            }).SingleOrDefault(c => c.Id == id);

            return View("DetailProd", product);
        }


        //Paginations

        public JsonResult Pagination(int page)
        {
            var products = GetProducts(page);

            return Json(products);
        }

        public PartialViewResult List(int page)
        {
            var products = GetProducts(page);

            return PartialView("_List", products);
        }

        private bool ProductExist(int id)
        {
            return _context.Product.Any(p => p.Id == id);
        }

        private List<Product> GetProducts(int page)
        {
            var skipCount = page * Consts.ProductPaginationCount;

            var products = _context.Product.Skip(skipCount)
                                   .Take(Consts.ProductPaginationCount)
                                   .ToList();
            return products;
        }
    }
}