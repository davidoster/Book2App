using Book2App.Models;
using System.Collections.Generic;

namespace Book2App.Repositories
{
    public interface IBookRepository
    {
        Book GetBookById(string bookId);
        Book Create(Book book);
        Book Update(Book book);
        Book Delete(string ID);

        IEnumerable<Book> AllBooks { get; }
        IEnumerable<Book> MostSelledBooks { get; }

    }
}
