using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Models;

namespace NiCeScanner.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Mail> Mail { get; set; } = default!;

		public DbSet<Question> Questions { get; set; } = default!;

		public DbSet<Category> Categories { get; set; } = default!;
	}
}
