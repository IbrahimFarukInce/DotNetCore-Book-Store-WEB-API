using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.Applications.BookOperations.Queries.GetBookDetail;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
         public IActionResult GetBooks()
         {
             GetBooksQuery query = new GetBooksQuery(_context,_mapper);
             var result = query.Handle();
             return Ok(result);
         }

         [HttpGet("{id}")]
         public IActionResult GetById(int id)
         {
             BookDetailViewModel result;

            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
         }

        [HttpPost]
        public IActionResult AddBook([FromBody]CreateBookModel NewBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = NewBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }    
            


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody]UpdateBookModel updatedBook)
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            command.Model = updatedBook;
            validator.ValidateAndThrow(command);   
            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]

        public IActionResult DeleteBook(int id, Book deletedBook)
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId=id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();       
            return Ok();
        }
         
         

    }


}