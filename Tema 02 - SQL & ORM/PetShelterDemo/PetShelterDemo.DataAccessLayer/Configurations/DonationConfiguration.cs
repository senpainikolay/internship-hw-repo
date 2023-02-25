using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Configurations;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.HasKey(p => p.Id);

        //Columns mapping and constraints
        builder.Property(p => p.Ammount).IsRequired();
        builder.Property(p => p.Currency).IsRequired();

        builder.Property(p => p.DonorId).IsRequired(); 
        builder.Property(p => p.FundraiserId).IsRequired(false);


        //Relationships 
        builder.HasOne(p => p.Donor)
            .WithMany(p => p.Donations)
            .HasForeignKey(p => p.DonorId);

        builder.HasOne(p => p.Fundraiser)
           .WithMany(p => p.Donations)
           .HasForeignKey(p => p.FundraiserId);

    }
}