using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Data.Interfaces;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeViewModel
            {
                CategorysCount = _categoryRepository.Count(x => true),
                BooksCount = _bookRepository.Count(x => true),
            };

            return View(homeVM);
        }
    }
}
