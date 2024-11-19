using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AuthorService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Author>> GetAuthors()
        {
            var targetDate = new DateTime(2015, 1, 1);
            return await _applicationDbContext.Authors
                .Where(author => author.Books.Any(book => book.PublishDate > targetDate))
                .Where(author => author.Books.Count() % 2 == 0)
                .ToListAsync();
        }

        public async Task<Author> GetAuthor()
        {
            var bookWithLongTitle = await _applicationDbContext.Books
                .OrderByDescending(book => book.Title.Length)
                .FirstOrDefaultAsync();

            return await _applicationDbContext.Authors
                .Where(author => author.Books.Any(book => book.Id == bookWithLongTitle.Id))
                .OrderBy(author => author.Id)
                .Include(author => author.Books)
                .FirstOrDefaultAsync();
        }
    }
}
