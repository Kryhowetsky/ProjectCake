using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectCake.Data;
using ProjectCake.Models;

namespace ProjectCake
{
    public class OrderCakesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderCakesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderCakes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderCake.ToListAsync());
        }

        // GET: OrderCakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCake = await _context.OrderCake
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderCake == null)
            {
                return NotFound();
            }

            return View(orderCake);
        }

        // GET: OrderCakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderCakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Phone,Email,Comment,ImageUrl,Date")] OrderCake orderCake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderCake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderCake);
        }

        // GET: OrderCakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCake = await _context.OrderCake.SingleOrDefaultAsync(m => m.Id == id);
            if (orderCake == null)
            {
                return NotFound();
            }
            return View(orderCake);
        }

        // POST: OrderCakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Phone,Email,Comment,ImageUrl,Date")] OrderCake orderCake)
        {
            if (id != orderCake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderCake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderCakeExists(orderCake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderCake);
        }

        // GET: OrderCakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCake = await _context.OrderCake
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderCake == null)
            {
                return NotFound();
            }

            return View(orderCake);
        }

        // POST: OrderCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderCake = await _context.OrderCake.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderCake.Remove(orderCake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderCakeExists(int id)
        {
            return _context.OrderCake.Any(e => e.Id == id);
        }
    }
}
