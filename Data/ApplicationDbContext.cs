using ASP_NET_Assignment1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ASP_NET_Assignment1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; } = null!;
        public virtual DbSet<BankAccount> BankAccount { get; set; } = null!;
        public virtual DbSet<ClientAccount> ClientAccount { get; set; } = null;

        public virtual DbSet<BankAccountType> BankAccountType { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientAccount>()
                .HasKey(ca => new { ca.ClientID, ca.AccountNum });

            modelBuilder.Entity<ClientAccount>()
                .HasOne(p => p.Client)
                .WithMany(p => p.ClientAccounts)
                .HasForeignKey(fk => new { fk.ClientID })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAccount>()
                .HasOne(p => p.BankAccount)
                .WithMany(p => p.ClientAccounts)
                .HasForeignKey(fk => new { fk.AccountNum })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankAccount>()
                .Property(p => p.Balance)
                .HasColumnType("decimal(9,2)");

            modelBuilder.Entity<BankAccount>()
                .Property(p => p.AccountType)
                .HasColumnType("varchar(15)");

            modelBuilder.Entity<Client>()
                .Property(p => p.FirstName)
                .HasColumnType("varchar(50)");

            modelBuilder.Entity<Client>()
                .Property(p => p.LastName)
                .HasColumnType("varchar(50)");

            modelBuilder.Entity<Client>()
                .Property(p => p.Email)
                .HasColumnType("varchar(50)");

            modelBuilder.Entity<BankAccountType>()
                .HasData(
                new BankAccountType { AccountType = "Chequing" },
                new BankAccountType { AccountType = "Saving" },
                new BankAccountType { AccountType = "Investment" },
                new BankAccountType { AccountType = "RRSP" },
                new BankAccountType { AccountType = "RESP" },
                new BankAccountType { AccountType = "TFSA" });
        }
    }
}