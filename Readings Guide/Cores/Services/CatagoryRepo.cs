using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Entities;
using Readings_Guide.Cores.Interfaces;

namespace Readings_Guide.Cores.Services
{
    public class CatagoryRepo : IRepository<CatagoryDto>
    {
        private readonly ApplicationDbContext context;

        public CatagoryRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<CatagoryDto> GetAll()
        {
            var result = context.Catagories.
                Select(e => new CatagoryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Descreption = e.Descreption,
                }).ToList();

            return result;
        }

        public CatagoryDto GetById(int id)
        {
            var result = context.Catagories.
                Select(e => new CatagoryDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Descreption = e.Descreption,
                }).FirstOrDefault(e => e.Id == id);

            return result;
        }

        public void Add(CatagoryDto entity)
        {
            var catagory = new Catagory
            {
                Id = entity.Id,
                Name = entity.Name,
                Descreption = entity.Descreption,
            };
            context.Catagories.Add(catagory);
            context.SaveChanges();
        }

        public void Update(int id, CatagoryDto entity)
        {
            var catagory = context.Catagories.FirstOrDefault(e => e.Id == id);
            catagory.Id = entity.Id;
            catagory.Name = entity.Name;
            catagory.Descreption = entity.Descreption;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var catagory = context.Catagories.FirstOrDefault(e => e.Id == id);
            context.Catagories.Remove(catagory);
            context.SaveChanges();
        }
       
    }
}
