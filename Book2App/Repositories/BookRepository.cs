using Book2App.Data;
using Book2App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book2App.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Book> AllBooks => _context.Book;

        public IEnumerable<Book> MostSelledBooks => throw new System.NotImplementedException();

        public BookRepository(ApplicationDbContext applicationDbContext)
        {

            _context = applicationDbContext;
        }

        public Book GetBookById(string bookId)
        {
            return _context.Book.Find(bookId);
        }

        public Book Create(Book book)
        {
            Book _book = _context.Add(book).Entity;
            _context.SaveChanges();
            return _book;
        }

        public Book Delete(string ID)
        {
           var book = _context.Book.Find(ID);
            if(book != null) 
            {
                var deletedBook = _context.Book.Remove(book);
                if (deletedBook.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
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
            // backup _book to the backupDB ELSEWHERE

            //await Task.Run(() => _context.SaveChangesAsync()); // admin app
            _context.SaveChanges(); // client app
            return _book;
        }
    }
}
