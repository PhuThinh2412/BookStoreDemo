using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreDemo.Models;
using BookStoreDemo.Models.Views;

namespace BookStoreDemo.DataAccess
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Book_ImportNote> Books_ImportNotes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ImportNote> ImportNotes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Receipt_Book> Receipts_Books { get; set; }
        public DbSet<Receipt_Coupon> Receipts_Coupons { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<V_Books> V_Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        //public DbSet<BookStoreDemo.Models.Book> Book { get; set; }
    }
}
