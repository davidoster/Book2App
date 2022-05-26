using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book2App.Data;
using Book2App.Models;
using Book2App.Repositories;

namespace Book2App.Controllers
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
            var books = _bookRepository.AllBooks;
            
            return View(await Task.Run(() => books));
        }

        // GET: Koukou
        public string Koukou()
        {
            string s = "<h1>Hello</h1>";
            return (s);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await Task.Run(() =>
                _bookRepository.GetBookById(id));
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
        public async Task<IActionResult> Create([Bind("ID,Title,ISBN,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _bookRepository.Create(book));
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await Task.Run(() => _bookRepository.GetBookById(id));
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
        public async Task<IActionResult> Edit(string id, [Bind("ID,Title,ISBN,Description")] Book book)
        {
            
            // it is not needed since we use EF Core BUT be on the safe side!!!!
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookRepository.Update(book);
                    //
                }
                // 1. deleted book
                // 2. changed id
                catch (DbUpdateConcurrencyException)
                {
                    // client app (id) 
                    // admin app (book.ID)
                    var _book = await Task.Run(() => _bookRepository.GetBookById(book.ID));
                    if (_book == null) 
                    {
                        return NotFound();
                    }
                    else
                    {
                        //throw; // admin app
                        RedirectToAction(nameof(Index)); // client app
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await Task.Run(() => _bookRepository.GetBookById(id));
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await Task.Run(() => _bookRepository.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        //private bool BookExists(string id)
        //{
        //    return _context.Book.Any(e => e.ID == id);
        //}
    }
}
