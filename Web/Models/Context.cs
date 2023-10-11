using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Web.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Bağlamayı yapılandır
            services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Diğer servis yapılandırmaları
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=DUYGU\\SQLEXPRESS; database=ExpenseApproval; integrated security=true; TrustServerCertificate=True;");

        //}
        
        public DbSet<User> Users { get; set; }
        public DbSet<ExpenseForm> ExpenseForms { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseDetail>()
                .Property(ef => ef.Amount)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<ExpenseForm>()
               .Property(ef => ef.TotalAmount)
               .HasColumnType("decimal(10,2)");
        }


    }
}
