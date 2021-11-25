using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context) { }

        public IEnumerable<Book> FindWithCategory(Func<Book, bool> predicate)
        {
            return _context.Books
                .Include(a => a.Category)
                .Where(predicate);
        }

        public IEnumerable<Book> FindWithCategoryAndBorrower(Func<Book, bool> predicate)
        {
            return _context.Books
                .Include(a => a.Category)
                .Include(a => a.Borrower)
                .Where(predicate);
        }

        public IEnumerable<Book> GetAllWithCategory()
        {
            return _context.Books.Include(a => a.Category);
        }
    }
}
