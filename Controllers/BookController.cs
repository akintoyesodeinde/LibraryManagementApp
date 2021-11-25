using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public BookController(IBookRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }
        [Route("Book")]
        public IActionResult List(int? categoryId)
        {

            var book = _repository.GetAllWithCategory().ToList();

            IEnumerable<Book> books;

            ViewBag.CategoryId = categoryId;


            if (categoryId == null)
            {
                books = _repository.GetAllWithCategory();
                return CheckBooksCount(books);
            }
            else
            {
                var category = _categoryRepository.GetWithBooks((int)categoryId);

                if (category.Books == null || category.Books.Count == 0)
                    return View("EmptyCategory", category);
            }

            books = _repository.FindWithCategory(a => a.Category.CategoryId == categoryId);

            return CheckBooksCount(books);
        }

        private IActionResult CheckBooksCount(IEnumerable<Book> books)
        {
            if (books == null || books.ToList().Count == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(books);
            }
        }

        public IActionResult Update(int id)
        {
            Book book = _repository.FindWithCategory(a => a.BookId == id).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            var bookVM = new BookEditViewModel
            {
                Book = book,
                Categorys = _categoryRepository.GetAll()
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(BookEditViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                bookVM.Categorys = _categoryRepository.GetAll();
                return View(bookVM);
            }
            _repository.Update(bookVM.Book);

            return RedirectToAction("List");
        }

        public IActionResult Create(int? categoryId)
        {
            Book book = new Book();

            if(categoryId != null)
            {
                book.CategoryId = (int)categoryId;
            }

            var bookVM = new BookEditViewModel
            {
                Categorys = _categoryRepository.GetAll(),
                Book = book
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookEditViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                bookVM.Categorys = _categoryRepository.GetAll();
                return View(bookVM);
            }

            _repository.Create(bookVM.Book);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id, int? categoryId)
        {
            var book = _repository.GetById(id);

            _repository.Delete(book);

            return RedirectToAction("List", new { categoryId = categoryId });
        }
    }
}
