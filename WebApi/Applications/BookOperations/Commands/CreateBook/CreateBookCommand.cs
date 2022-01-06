using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using WebApi.Entities;


namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    
    public class CreateBookCommand
    {
        public CreateBookModel Model;//ViewModel
        private readonly BookStoreDbContext _dbcontext;//DB
        private readonly IMapper _mapper;//AutoMapper
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(book => book.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Kitap Zaten Mevcut");
            }

            book = _mapper.Map<Book>(Model);//Model'i Book Objesine Çevirir(AutoMapper)
            _dbcontext.Add(book);
            _dbcontext.SaveChanges();  
        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}