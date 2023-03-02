
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace PetShelter.Domain;

public class Fundraiser : INamedEntity
{
        public string Name { get; set; }
        public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; }

    public DateTime DueDate { get; set; }  
         public string Status { get; set; }  
        public ICollection<Person> Donors { get; set; }
       

    public Fundraiser(string name, decimal goalAmount, DateTime dueDate )
    {  

        Name = name;
        GoalAmount = goalAmount;
        DueDate = dueDate; 
        Donors= new List<Person>();
    }
    
    public Fundraiser(string name, decimal goalAmount, DateTime dueDate, string status, decimal currentAmount)
    { 
        Name = name;
        GoalAmount = goalAmount;
        DueDate = dueDate;
        Status = status;
        CurrentAmount = currentAmount; 
        Donors = new List<Person>();

    }
}
