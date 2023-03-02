using System.Runtime.CompilerServices; 


namespace PetShelter.Domain.Extensions.DomainModel
{
    internal static class FundraiserExtensions
    {
        public static Fundraiser ToDomainModel(this DataAccessLayer.Models.Fundraiser fundraiser)
        {
            if (fundraiser == null)
            {
                return null;
            }

            return new Fundraiser(fundraiser.Name, fundraiser.GoalAmount, fundraiser.DueDate, fundraiser.Status, fundraiser.CurrentAmount);
        }
     

        public static  DataAccessLayer.Models.Fundraiser FromDomainModel(this Fundraiser fundraiser, DataAccessLayer.Models.Person  person)
        {
            var entity = new DataAccessLayer.Models.Fundraiser
            {
                Name = fundraiser.Name,  
                DueDate = fundraiser.DueDate,
                GoalAmount= fundraiser.GoalAmount,
                OwnerId = person.Id,
                Owner = person 
            }; 
            return entity;
        }
    }
}
