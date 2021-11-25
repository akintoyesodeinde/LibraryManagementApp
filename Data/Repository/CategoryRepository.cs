using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace LibraryManagement.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDbContext context) : base(context) {}

        public IEnumerable<Category> GetAllWithBooks()
        {
            return _context.Categorys.Include(a => a.Books);
        }

        public Category GetWithBooks(int id)
        {
            return _context.Categorys.Where(a => a.CategoryId == id).Include(a => a.Books).FirstOrDefault();
        }

        public override void Delete(Category entity)
        {
            // https://github.com/aspnet/EntityFrameworkCore/issues/3924
            // EF Core 2 doesnt support Cascade on delete for in Memory Database

            var booksToRemove = _context.Books.Where(b => b.Category == entity);

            base.Delete(entity);

            _context.Books.RemoveRange(booksToRemove);

            Save();
        }
    }
}
