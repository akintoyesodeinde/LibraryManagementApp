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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }
        [Route("Category")]
        public IActionResult List()
        {
            if (!_repository.Any()) return View("Empty");

            var categorys = _repository.GetAllWithBooks();

            return View(categorys);
        }

        public IActionResult CategoryDetail()
        {
            var categorys = _repository.GetAllWithBooks();

            if (categorys?.ToList().Count == 0)
            {
                return View("Empty");
            }

            return View(categorys);
        }

        public IActionResult Detail(int id)
        {
            var category = _repository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Update(int id)
        {
            var category = _repository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _repository.Update(category);

            return RedirectToAction("List");
        }

        public ViewResult Create()
        {
            return View(new CreateCategoryViewModel { Referer = Request.Headers["Referer"].ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            _repository.Create(categoryVM.Category);

            if (!String.IsNullOrEmpty(categoryVM.Referer))
            {
                return Redirect(categoryVM.Referer);
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var customer = _repository.GetById(id);

            _repository.Delete(customer);

            return RedirectToAction("List");
        }
    }
}
