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

    //public static Person AsResource(this Domain.Person person)
    //{
    //    return new Person
    //    {
    //        DateOfBirth = person.DateOfBirth,
    //        IdNumber = person.IdNumber,
    //        Name = person.Name,
    //    };
    //}
}