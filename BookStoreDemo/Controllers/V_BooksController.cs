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
    public class V_BooksController : Controller
    {
        private readonly PostgreSqlContext _context;

        public V_BooksController(PostgreSqlContext context)
        {
            _context = context;
        }

        // GET: V_Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.V_Books.ToListAsync());
        }

        // GET: V_Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var v_Books = await _context.V_Books
                .FirstOrDefaultAsync(m => m.ID == id);
            if (v_Books == null)
            {
                return NotFound();
            }

            return View(v_Books);
        }

        // GET: V_Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: V_Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MaSach,TenSach,DonGia,TenTacGia,TenNXB")] V_Books v_Books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(v_Books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(v_Books);
        }

        // GET: V_Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var v_Books = await _context.V_Books.FindAsync(id);
            if (v_Books == null)
            {
                return NotFound();
            }
            return View(v_Books);
        }

        // POST: V_Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MaSach,TenSach,DonGia,TenTacGia,TenNXB")] V_Books v_Books)
        {
            if (id != v_Books.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(v_Books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!V_BooksExists(v_Books.ID))
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
            return View(v_Books);
        }

        // GET: V_Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var v_Books = await _context.V_Books
                .FirstOrDefaultAsync(m => m.ID == id);
            if (v_Books == null)
            {
                return NotFound();
            }

            return View(v_Books);
        }

        // POST: V_Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var v_Books = await _context.V_Books.FindAsync(id);
            _context.V_Books.Remove(v_Books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool V_BooksExists(int id)
        {
            return _context.V_Books.Any(e => e.ID == id);
        }
    }
}
