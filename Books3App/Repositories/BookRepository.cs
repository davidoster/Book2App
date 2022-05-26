using Books3App.Data;
using Books3App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Books3App.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Book> AllBooks => _context.Book;
        //{ get 
        //    {
        //        return _context.Book.ToList();
        //    } 
        //}        

        public IEnumerable<Book> MostSelledBooks { get 
            {
                return _context.Book.OrderByDescending(b => b.Sales);
                //return _context.Set<Book>().OrderByDescending(b => b.Sales).Take(5);
            } 
        } 

        public Book GetById(int? id)
        {
            return _context.Book.Find(id);
        }

        public Book Create(Book book)
        {
            Book _book = _context.Book.Add(book).Entity;
            _context.SaveChanges();
            return _book;
        }

        public Book Delete(int? id)
        {
            var book = _context.Book.Find(id);
            if(book != null)
            {
                var deletedBook = _context.Book.Remove(book);
                if (deletedBook.State == EntityState.Deleted)
                {
                    _context.SaveChanges();
                    return deletedBook.Entity;
                }
            }            
            return null;
        }


        public Book Update(Book book)
        {
            var _book = _context.Update(book).Entity;

            //await Task.Run(() => _context.SaveChangesAsync()); //admin app

            _context.SaveChanges(); //client app
            return _book;
        }        
    }
}