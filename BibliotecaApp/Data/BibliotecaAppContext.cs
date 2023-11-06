using BibliotecaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace BibliotecaApp.Data
{
    public class BibliotecaAppContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BibliotecaAppContext(DbContextOptions<BibliotecaAppContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Livro> Livro { get; set; } = default!;

        public DbSet<Usuario> Usuario { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbSettings = _configuration.GetSection("DatabaseSettings");

            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = dbSettings["Server"],
                InitialCatalog = dbSettings["Database"],
                IntegratedSecurity = bool.Parse(dbSettings["TrustedConnection"]!),
                MultipleActiveResultSets = bool.Parse(dbSettings["MultipleActiveResultSets"]!)
            };

            optionsBuilder.UseSqlServer(connectionString.ToString());
        }
    }
}
