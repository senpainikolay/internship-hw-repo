using Microsoft.EntityFrameworkCore;
using PetShelterDemo.DataAccessLayer.Configurations;
using PetShelterDemo.DataAccessLayer.Models;
using System.Diagnostics.Metrics;

namespace PetShelterDemo.DataAccessLayer;

public class PetShelterContext : DbContext
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Fundraiser> Fundraisers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=PetShelter;Trusted_Connection=True;TrustServerCertificate=True");
       
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PetConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new DonationConfiguration());
        modelBuilder.ApplyConfiguration(new FundraiserConfiguration());
    }
} 