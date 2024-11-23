
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Interfaces;
using Readings_Guide.Cores.Services;

namespace Readings_Guide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReadingDB;Integrated Security=True")
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IRepository<BookDto>, BookRepo>();
            builder.Services.AddScoped<IRepository<CatagoryDto>, CatagoryRepo>();
            builder.Services.AddScoped<IAuth, AuthService>();
                

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
