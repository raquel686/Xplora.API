using Microsoft.EntityFrameworkCore;
using XploraAPI.PuntosDeVenta.Domain.Models;
using XploraAPI.Security.Domain.Models;

namespace XploraAPI.Shared.Persistence.Contexts;


public class AppDbContext:DbContext
{
  
    public DbSet<User> Users { get; set; }
    
    public DbSet<PDV> Pdvs {get;set;}
    
    public DbSet<Product> Products{get;set;}
    
    //Not implemented Yet
    //public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //USERS
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p=>p.Id);
        builder.Entity<User>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p=>p.FirstName).IsRequired();
        builder.Entity<User>().Property(p=>p.LastName).IsRequired();
        builder.Entity<User>().Property(p=>p.Email).IsRequired().HasMaxLength(30);
        
        builder.Entity<PDV>().ToTable("Pdvs");
        builder.Entity<PDV>().HasKey(p => p.Id);
        builder.Entity<PDV>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PDV>().Property(p => p.Code).IsRequired();
        builder.Entity<PDV>().Property(p => p.Direction).IsRequired();
        builder.Entity<PDV>().Property(p => p.Name).IsRequired();
        builder.Entity<PDV>().Property(p => p.Latitude).IsRequired();
        builder.Entity<PDV>().Property(p => p.Longitude).IsRequired();

        builder.Entity<PDV>()
            .HasMany(p => p.products)
            .WithOne(p => p.PDV)
            .HasForeignKey(p => p.PDVId);


        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Stock).IsRequired();
        builder.Entity<Product>().Property(p => p.PCosto).IsRequired();
        builder.Entity<Product>().Property(p => p.PRvtaMayor).IsRequired();
        builder.Entity<Product>().Property(p => p.Name).IsRequired();






    }
}