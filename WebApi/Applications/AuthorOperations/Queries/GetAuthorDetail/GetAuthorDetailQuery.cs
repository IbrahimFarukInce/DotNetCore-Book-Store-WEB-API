using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId;
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailsViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı!");
            }
            AuthorDetailsViewModel vm = _mapper.Map<AuthorDetailsViewModel>(author);
            return vm;
        }
    }
    public class AuthorDetailsViewModel
    {
        public int Id { get; set; }
        public  string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}