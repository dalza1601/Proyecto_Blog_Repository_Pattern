using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data;

namespace Proyecto_Blog.DL.DA.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider) {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //Aplicar migraciones pendientes
            context.Database.Migrate();

            //Evitar ejecutar el seeding si ya hay datos en las tablas
            if (context.Categories.Any() || context.Articles.Any() || context.Sliders.Any())
            {
                return; // La base de datos ya ha sido poblada
            }

            var rnd = new Random();

            var categoryNames = new[] { "Tecnología", "Salud", "Viajes", "Gastronomía", "Deportes" };
            
            var categories = categoryNames.Select((name, index) => new Category
            {
                Name = name,
                Orden = index + 1
            }).ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            //Creamos Artículos de ejemplo
            var articleNames = new[] { "Cómo aprender C#", "10 consejos para una vida saludable", "Los mejores destinos de viaje en 2024", "Recetas fáciles para principiantes", "Los deportes más populares del mundo" };
            var articles = articleNames.Select((name, index) => new Article
            {
                Name = name,
                Description = $"Contenido del artículo {index + 1}",
                ImageUrl = $"https://picsum.photos/seed/art{index + 1}/800/450",
                CategoryId = categories[rnd.Next(categories.Count)].Id
            }).ToList();

            context.Articles.AddRange(articles);
            context.SaveChanges();

            //Creamos Sliders de ejemplo
            var sliderNames = new[] { "Slider 1", "Slider 2", "Slider 3" };
            var sliders = sliderNames.Select((name, index) => new Slider
            {
                Name = name,
                IsActive = index % 2 == 0, // Activamos solo los sliders pares
                Url = $"https://picsum.photos/seed/slider{index + 1}/1200/400"
            }).ToList();
            context.Sliders.AddRange(sliders);
            context.SaveChanges();

        }
    }
}
