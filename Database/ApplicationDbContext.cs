using Microsoft.EntityFrameworkCore;
using YprojectTranslateService.TranslationFolder.Entity;

namespace YprojectTranslateService.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Translation> Translations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
