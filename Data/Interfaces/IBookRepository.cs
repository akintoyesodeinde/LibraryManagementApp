using LibraryManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAllWithCategory();
        IEnumerable<Book> FindWithCategory(Func<Book, bool> predicate);
        IEnumerable<Book> FindWithCategoryAndBorrower(Func<Book, bool> predicate);
    }
}
