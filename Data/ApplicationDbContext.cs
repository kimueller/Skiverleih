using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skiverleih.Models;

namespace Skiverleih.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OnLoan> OnLoans { get; set; }
    }
}