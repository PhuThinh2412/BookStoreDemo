using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreDemo.Models;

namespace BookStoreDemo.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddAuthorRecord(Author patient);
        void UpdateAuthorRecord(Author patient);
        void DeleteAuthorRecord(int id);
        Author GetAuthorSingleRecord(int id);
        List<Author> GetAuthorRecords();
    }
}
