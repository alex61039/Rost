using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository
{
    public class RostDbContext : IdentityDbContext<ApplicationUser>
    {
        public RostDbContext(DbContextOptions<RostDbContext> options) : base(options)
        {
        }  
        
        public DbSet<Career> Careers { get; set; }
        public DbSet<CareerDirection> CareerDirections { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<MunicipalUnion> MunicipalUnions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Structure> Structures { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
    }
}