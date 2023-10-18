using Microsoft.EntityFrameworkCore;


namespace MvcMovie.Database
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Movie> Movie { get; set; } = default!;
    }
}
