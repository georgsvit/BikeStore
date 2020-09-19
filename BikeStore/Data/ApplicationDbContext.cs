using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AgeGroup> AgeGroups { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<FrameSize> FrameSizes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelColour> ModelColours { get; set; }
        public DbSet<ModelName> ModelNames { get; set; }
        public DbSet<ModelPrefix> ModelPrefixes { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StoringPlace> StoringPlaces { get; set; }
        public DbSet<SupplyDetail> SupplyDetails { get; set; }
        public DbSet<SupplyHeader> SupplyHeaders { get; set; }
        public DbSet<Suspension> Suspensions { get; set; }
    }
}
