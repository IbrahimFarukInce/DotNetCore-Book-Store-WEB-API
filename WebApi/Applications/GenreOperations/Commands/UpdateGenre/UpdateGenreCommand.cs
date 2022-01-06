using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId;
        private readonly BookStoreDbContext _context;
        public UpdateGenreModel Model;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kategori Bulunamadı!");
            }
            if (_context.Genres.Any(genre => genre.Name.ToLower() == Model.Name.ToLower() && genre.Id != GenreId))
            {
                throw new InvalidOperationException("Bu Kategori Zaten Mevcut!");
            }
            genre.Name = String.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name ;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}