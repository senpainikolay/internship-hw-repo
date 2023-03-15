using System.Collections.Immutable;

namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtentions
{
    public static Domain.Fundraiser AsDomainModel(this Fundraiser fundraiser)
    {
        return new Domain.Fundraiser(  
            fundraiser.Name, 
            fundraiser.GoalAmount, 
            fundraiser.DueDate 
            );
    }
    public static FundraiserMinimalInfo AsMinimalResource(this Domain.Fundraiser fundraiser)
    {
        return new FundraiserMinimalInfo(
            fundraiser.Name,
            fundraiser.Status
            );
    }
    public static FundraiserWithDonors AsResource(this Domain.Fundraiser domainFundraiser)
    {
        return new FundraiserWithDonors(
           domainFundraiser.Name,
           domainFundraiser.DueDate,
          domainFundraiser.GoalAmount,
          domainFundraiser.Donors.Select(d => d.AsResource()).ToImmutableArray(),
          domainFundraiser.Status,
          domainFundraiser.CurrentAmount
           );  
        
    }
}