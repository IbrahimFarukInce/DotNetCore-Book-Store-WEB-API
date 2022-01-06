using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == Model.Name);
            if (genre is not null)
            {
                throw new InvalidOperationException("Bu Kategori Zaten Mevcut!");
            }
            genre = new Genre();
            genre.Name = Model.Name;
            _context.Add(genre);
            _context.SaveChanges();
        }
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }

        public static implicit operator CreateGenreModel(CreateGenreCommand v)
        {
            throw new NotImplementedException();
        }
    }
}