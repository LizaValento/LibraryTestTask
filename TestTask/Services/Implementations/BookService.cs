using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Book>> GetBooks()
        {
            var targetDate = new DateTime(2012, 5, 22);
            return await _applicationDbContext.Books
                .Where(book => book.Title.Contains("Red") && book.PublishDate > targetDate)
                .ToListAsync();
        }

        public async Task<Book> GetBook()
        {
            return await _applicationDbContext.Books
                .Where(book => book.Price == _applicationDbContext.Books.Max(b => b.Price))
                .FirstOrDefaultAsync();
        }
    }
}
