using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            
            if (context.Books.Any())
            {
                return;   
            }
            context.Genres.AddRange(
                new Genre{
                    Name = "Personal Growth"
                },
                new Genre{
                    Name = "Fantasy"
                },
                new Genre{
                    Name = "Scince Fiction"
                }
            );
            context.Authors.AddRange(
                new Author{
                    FirstName ="Eric",
                    LastName ="Ries",
                    DateOfBirth = new DateTime(1978,09,09)
                },
                new Author{
                    FirstName ="JRR",
                    LastName ="Tolken",
                    DateOfBirth = new DateTime(1978,09,09)
                },
                new Author{
                    FirstName ="Frank",
                    LastName ="Herbert",
                    DateOfBirth = new DateTime(1978,09,09)
                }


            );

            context.Books.AddRange(
               new Book{
                 Title = "Lean Startup",
                 GenreId = 1,
                 AuthorId = 1,
                 PageCount = 250,
                 PublishDate = new DateTime(2001,06,12)
             },
             new Book{
                 Title = "Lors Of The Rings",
                 GenreId = 2,
                 AuthorId = 2,
                 PageCount = 1100,
                 PublishDate = new DateTime(2011,01,21)
             },
             new Book{
                 Title = "Dune",
                 GenreId = 3,
                 AuthorId =3,
                 PageCount = 720,
                 PublishDate = new DateTime(2000,05,22)
             });

            context.SaveChanges();
        }
    }
}
}