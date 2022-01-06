using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorViewModel Model;
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.FirstName.ToLower() == Model.FirstName.ToLower() && author.LastName.ToLower() == Model.LastName.ToLower());
            if(author is not null)
            {
                throw new InvalidOperationException("Yazar Zaten Mevcut");
            }
            author = _mapper.Map<Author>(Model);
            _context.Add(author);
            _context.SaveChanges();
            
        }
    }
    public class CreateAuthorViewModel
    {
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}