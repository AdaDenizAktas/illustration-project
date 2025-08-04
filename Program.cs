using IllustrationProject.Data;
using IllustrationProject.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace IllustrationProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var sqliteConnection = new SqliteConnection("DataSource=:memory:");
            sqliteConnection.Open();
            builder.Services.AddSingleton(sqliteConnection);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(sqliteConnection)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine));

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
