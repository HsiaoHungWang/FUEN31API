
using FUEN31API.Models;
using Microsoft.EntityFrameworkCore;

namespace FUEN31API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddXmlSerializerFormatters();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<NorthwindContext>(
             options => options.UseSqlServer(
                 builder.Configuration.GetConnectionString("NorthwindConnection")
         ));

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

           
        }
    }
}
