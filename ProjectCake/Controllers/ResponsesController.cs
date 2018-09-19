using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCake.Data;
using ProjectCake.Models;

namespace ProjectCake.Controllers
{
    public class ResponsesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ResponsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TestIndex()
        {
            var list = await _context.Respond.ToListAsync();
            return Json(list);
        }

        public async Task<IActionResult> TestCreate()
        {
            var respond = new Respond
            {
                Name = "1",
                Text = "2",
                Email = "dd@d.d",
                ProductId = 1
            };
            var addedRespond = _context.Add(respond);
            await _context.SaveChangesAsync();

            return Json(addedRespond);
        }

        public async Task<IActionResult> Create(RespondViewModel viewModel)
        {
            var respond = new Respond
            {
                Name = viewModel.Name,
                Text = viewModel.Text,
                Email = viewModel.Email,
                ProductId = viewModel.ProductId,
                AddedDate = DateTime.Now
            };

            _context.Add(respond);
            await _context.SaveChangesAsync();

            return RedirectToAction("DetailProd", "Product", new { id = viewModel.ProductId });
        }

    }
}