using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books3App.Data;
using Books3App.Models;
using Books3App.Repositories;

namespace Books3App.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => _bookRepository.AllBooks));
        }

        public async Task<IActionResult> Most()
        {
            return View(await Task.Run(() => _bookRepository.MostSelledBooks));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await Task.Run(() => _bookRepository.GetById(id));
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ISBN,Description,Sales")] Book book)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _bookRepository.Create(book));
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await Task.Run(() => _bookRepository.GetById(id));
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ISBN,Description,Sales")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookRepository.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var _book = await Task.Run(() => _bookRepository.GetById(book.Id));
                    if (_book == null)
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await Task.Run(() => _bookRepository.GetById(id));
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var book = await Task.Run(() => _bookRepository.GetById(id));
            var book = await Task.Run(() => _bookRepository.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        //private bool BookExists(int id)
        //{
        //    return _context.Book.Any(e => e.Id == id);
        //}
    }
}
