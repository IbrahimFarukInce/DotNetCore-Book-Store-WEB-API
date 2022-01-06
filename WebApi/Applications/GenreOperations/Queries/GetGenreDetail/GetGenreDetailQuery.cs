using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public  GenreDetailViewModel Handle()
        {
            var Genre = _context.Genres.SingleOrDefault(genre => genre.IsActive && genre.Id == GenreId);
            if (Genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }
            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(Genre);
            return vm ;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}