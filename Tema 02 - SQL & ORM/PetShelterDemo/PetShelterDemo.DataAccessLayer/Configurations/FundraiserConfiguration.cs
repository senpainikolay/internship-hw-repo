using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelterDemo.DataAccessLayer.Models;
using System.Reflection.Emit;


namespace PetShelterDemo.DataAccessLayer.Configurations;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
    public void Configure(EntityTypeBuilder<Fundraiser> builder)
    {
        builder.HasKey(p => p.Id);

        //Columns mapping and constraints
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).IsRequired();

        builder.Property(p => p.DonationTarget).IsRequired();
        builder.Property(p => p.DonationCurrency).IsRequired();


        //Relationships

        builder.HasMany(p => p.Donations)
            .WithOne(p => p.Fundraiser)
            .HasForeignKey(p => p.FundraiserId)
             .IsRequired();
    }
}