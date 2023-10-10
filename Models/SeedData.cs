using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // looking for movies
                if (context.Movie.Any())
                {
                    return; // Db has been seeded

                }

                context.Movie.AddRange(

                    new Movie
                    {
                        Title = "RRR",
                        ReleaseDate = DateTime.Parse("2022-02-12"),
                        Genre = "Action",
                        Price = 3.21M,
                        Rating = "B"
                    },

                 new Movie
                 {
                     Title = "Fun and fun",
                     ReleaseDate = DateTime.Parse("2002-08-10"),
                     Genre = "Comedy",
                     Price = 8.2M,
                     Rating = "A"
                 },


                 new Movie
                 {
                     Title = "KGF 2",
                     ReleaseDate = DateTime.Parse("2015-06-11"),
                     Genre = "Horror",
                     Price = 3.12M,
                     Rating = "C"
                 },



                 new Movie
                 {
                     Title = "KGF",
                     ReleaseDate = DateTime.Parse("2009-05-17"),
                     Genre = "Horror",
                     Price = 1.12M,
                     Rating = "A"
                 },


                 new Movie
                 {
                     Title = "Great Hills",
                     ReleaseDate = DateTime.Parse("2012-12-05"),
                     Genre = "Thriller",
                     Price = 4.52M,
                     Rating = "D"

                 }
                );

                context.SaveChanges();
            }
        }
    }
}
