using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
    public void Configure(EntityTypeBuilder<Fundraiser> builder)
    {
        //Primary key
        builder.HasKey(p => p.Id);

        //Columns mapping and constraints
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.GoalAmount).IsRequired();
        builder.Property(p => p.CurrentAmount).HasDefaultValue(0.0).IsRequired(); 
        builder.Property(p => p.Status).HasDefaultValue("Active").IsRequired().HasMaxLength(10);
        builder.Property(p => p.CreationDate).HasDefaultValue(DateTime.Now).IsRequired();
        builder.Property(p => p.DueDate).IsRequired();


        //relantionships  
        builder.HasOne(p => p.Owner).WithMany(p => p.FundraisersOwned).HasForeignKey(p => p.OwnerId)
           .IsRequired();


    }
}

