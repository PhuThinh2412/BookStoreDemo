using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreDemo.DataAccess;
using BookStoreDemo.Models.Views;

namespace BookStoreDemo.Controllers
{
    public class V_RevenueOnBookController : Controller
    {
        private readonly PostgreSqlContext _context;

        public V_RevenueOnBookController(PostgreSqlContext context)
        {
            _context = context;
        }

        // GET: V_RevenueOnBook
        public async Task<IActionResult> Index()
        {
            return View(await _context.V_RevenueOnEachBook.ToListAsync());
        }

        public async Task<IActionResult> RevueneByTime(string? time)
        {
            var milestons = time == null ? "2021-01-01 00:00:00" : time;
            var query = $"SELECT * FROM func_get_revenue_on_each_book_by_time('"+ milestons + "')";
            var list = _context.RevenueEachBookByTime.FromSqlRaw(query);
            return View(await list.ToListAsync());
        }

        // GET: V_RevenueOnBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var v_RevenueOnBook = await _context.V_RevenueOnEachBook
                .FirstOrDefaultAsync(m => m.ID == id);
            if (v_RevenueOnBook == null)
            {
                return NotFound();
            }

            return View(v_RevenueOnBook);
        }

        // GET: V_RevenueOnBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: V_RevenueOnBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MaSach,TenSach,DonGia,tongsobanra,doanhthu")] V_RevenueOnBook v_RevenueOnBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(v_RevenueOnBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(v_RevenueOnBook);
        }

        // GET: V_RevenueOnBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var v_RevenueOnBook = await _context.V_RevenueOnEachBook.FindAsync(id);
            if (v_RevenueOnBook == null)
            {
                return NotFound();
            }
            return View(v_RevenueOnBook);
        }

        // POST: V_RevenueOnBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MaSach,TenSach,DonGia,tongsobanra,doanhthu")] V_RevenueOnBook v_RevenueOnBook)
        {
            if (id != v_RevenueOnBook.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(v_RevenueOnBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!V_RevenueOnBookExists(v_RevenueOnBook.ID))
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
            return View(v_RevenueOnBook);
        }

        // GET: V_RevenueOnBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var v_RevenueOnBook = await _context.V_RevenueOnEachBook
                .FirstOrDefaultAsync(m => m.ID == id);
            if (v_RevenueOnBook == null)
            {
                return NotFound();
            }

            return View(v_RevenueOnBook);
        }

        // POST: V_RevenueOnBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var v_RevenueOnBook = await _context.V_RevenueOnEachBook.FindAsync(id);
            _context.V_RevenueOnEachBook.Remove(v_RevenueOnBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool V_RevenueOnBookExists(int id)
        {
            return _context.V_RevenueOnEachBook.Any(e => e.ID == id);
        }
    }
}
