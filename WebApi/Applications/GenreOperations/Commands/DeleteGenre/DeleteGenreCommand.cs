using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId;
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            else if(_context.Books.Any(x => x.GenreId == GenreId))
                throw new InvalidOperationException("Silinemedi, Kategoriye Ait Kitaplar Bulundu!");
            _context.Remove(genre);
            _context.SaveChanges();
        }
        
    }
}    