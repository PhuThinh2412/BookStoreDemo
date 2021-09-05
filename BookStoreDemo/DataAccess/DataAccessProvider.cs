using BookStoreDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreDemo.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public void AddAuthorRecord(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void DeleteAuthorRecord(int id)
        {
            var entity = _context.Authors.FirstOrDefault(t => t.ID == id);
            _context.Authors.Remove(entity);
            _context.SaveChanges();
        }

        public List<Author> GetAuthorRecords()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthorSingleRecord(int id)
        {
            return _context.Authors.FirstOrDefault(t => t.ID == id);
        }


        public void UpdateAuthorRecord(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }
    }
}
