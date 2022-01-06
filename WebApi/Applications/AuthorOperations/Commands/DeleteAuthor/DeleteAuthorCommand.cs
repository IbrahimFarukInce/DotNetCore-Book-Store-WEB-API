using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId;
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Silinecek Yazar Bulunamadı!");
            }
            else if(_context.Books.Any(x => x.AuthorId == AuthorId))
                throw new InvalidOperationException("Silinemedi, Yazara Ait Kitaplar Bulundu!");
            _context.Remove(author);
            _context.SaveChanges();
        }
        
    }
}