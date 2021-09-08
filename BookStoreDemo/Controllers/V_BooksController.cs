using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreDemo.DataAccess;
using BookStoreDemo.Models.Views;
using System.Data;

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
        public async Task<IActionResult> BuyOne(int idbook)
        {
            using var transaction = _context.Database.BeginTransaction();
            var book = _context.V_Books.Where(v => v.ID == idbook).FirstOrDefault();
            var lastReceipt = _context.Receipts.OrderByDescending(v => v.ID).First();
            var mahoadon = lastReceipt.ID + 1;
            var mahoadonstr = "HD" + mahoadon;
            var ngayban = DateTime.Now;
            var thanhtien = Decimal.Parse(book.DonGia.ToString());
            var customer = 1;

            var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "func_add_receipt";
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_mahoadon", NpgsqlTypes.NpgsqlDbType.Text) { Value = mahoadonstr });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_ngayban", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = ngayban });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_thanhtien", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = thanhtien });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_idcustomer", NpgsqlTypes.NpgsqlDbType.Integer) { Value = customer });
            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            var receiptid = command.ExecuteScalar();
            await _context.SaveChangesAsync();


            command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "func_add_book_to_receipt";
            command.Parameters.Add(new Npgsql.NpgsqlParameter("vid_receipt", NpgsqlTypes.NpgsqlDbType.Integer) { Value = receiptid });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("vbook_id", NpgsqlTypes.NpgsqlDbType.Integer) { Value = book.ID });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("vsoluongban", NpgsqlTypes.NpgsqlDbType.Integer) { Value = 1 });
            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            var rc_book = command.ExecuteScalar();
            await _context.SaveChangesAsync();
            transaction.Commit();
            //var id = _context.Database.ExecuteSqlRawAsync("SELECT func_add_book_to_receipt({0},{1},{2});", receiptid, book.ID, 1);

            return RedirectToAction("Index", "V_Books");
        }
        public async Task<IActionResult> BuyAll (string buyingList, string tongtien)
        {
            using var transaction = _context.Database.BeginTransaction();
            //var book = _context.V_Books.Where(v => v.ID == idbook).FirstOrDefault();
            var lastReceipt = _context.Receipts.OrderByDescending(v => v.ID).First();
            var mahoadon = lastReceipt.ID + 1;
            var mahoadonstr = "HD" + mahoadon;
            var ngayban = DateTime.Now;
            var customer = 1;

            var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "func_add_receipt";
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_mahoadon", NpgsqlTypes.NpgsqlDbType.Text) { Value = mahoadonstr });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_ngayban", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = ngayban });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_thanhtien", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = Decimal.Parse(tongtien) });
            command.Parameters.Add(new Npgsql.NpgsqlParameter("v_idcustomer", NpgsqlTypes.NpgsqlDbType.Integer) { Value = customer });
            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            var receiptid = command.ExecuteScalar();
            await _context.SaveChangesAsync();
             
            var affected = _context.Database.ExecuteSqlRawAsync("CALL prod_add_books_to_receipt({0}, {1}, {2})", receiptid, buyingList, buyingList);
            await _context.SaveChangesAsync();
            transaction.Commit();

            return RedirectToAction("Index", "V_Books");
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
