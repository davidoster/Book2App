using Books3App.Data;
using Books3App.Models;
using System.Collections.Generic;

namespace Books3App.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> AllBooks { get; }
        IEnumerable<Book> MostSelledBooks { get; } 

        Book GetById(int? id);
        Book Create(Book book);
        Book Delete(int? id);
        Book Update(Book book);
    }
}
