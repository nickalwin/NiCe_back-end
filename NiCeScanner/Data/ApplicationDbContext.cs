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

		public DbSet<Scan> Scans { get; set; } = default!;

		public DbSet<Answer> Answers { get; set; } = default!;

		public DbSet<Opinion> Opinions { get; set; } = default!;

		public DbSet<Sector> Sectors { get; set; } = default!;

		public DbSet<ScanCode> ScanCodes { get; set; } = default!;

		public DbSet<ImageModel> Images { get; set; } = default!;
	}
}
