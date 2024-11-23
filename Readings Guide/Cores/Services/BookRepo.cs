using Microsoft.EntityFrameworkCore;
using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Entities;
using Readings_Guide.Cores.Interfaces;

namespace Readings_Guide.Cores.Services
{
    public class BookRepo : IRepository<BookDto>
    {
        private readonly ApplicationDbContext context;

        public BookRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<BookDto> GetAll()
        {
            var result = context.Books.
                Include(e => e.catagory)
                .Select(e => new BookDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Descreption = e.Descreption,
                    Auther = e.Auther,
                    CatagoryId = e.CatagoryId,
                    ImageUrl = e.ImageUrl,
                    PublicTime = e.PublicTime,
                }).ToList();

            return result;
        }

        public BookDto GetById(int id)
        {
            var result = context.Books.
                Select(e => new BookDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Descreption = e.Descreption,
                    Auther = e.Auther,
                    CatagoryId = e.CatagoryId,
                    ImageUrl = e.ImageUrl,
                    PublicTime = e.PublicTime,
                }).FirstOrDefault(e => e.Id == id);

            return result;
        }

        public void Add(BookDto entity)
        {
            var book = new Book
            {
                Id = entity.Id,
                Title = entity.Title,
                Descreption = entity.Descreption,
                Auther = entity.Auther,
                CatagoryId = entity.CatagoryId,
                ImageUrl = entity.ImageUrl,
                PublicTime = entity.PublicTime,
            };
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void Update(int id, BookDto entity)
        {
            
            var book = context.Books.FirstOrDefault(e => e.Id == id);
            book.Id = entity.Id;
            book.Title = entity.Title;
            book.Descreption = entity.Descreption;
            book.Auther = entity.Auther;
            book.CatagoryId = entity.CatagoryId;
            book.ImageUrl = entity.ImageUrl;
            book.PublicTime = entity.PublicTime;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = context.Books.FirstOrDefault(e => e.Id == id);
            context.Books.Remove(book);
            context.SaveChanges();
        }
        
    }
}
