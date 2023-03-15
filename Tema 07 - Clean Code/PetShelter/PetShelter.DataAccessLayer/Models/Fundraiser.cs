namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }


    public decimal CurrentAmount { get; set; }
    public decimal GoalAmount { get; set; }

    public string Status { get; set; }


    public DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }

    public int  OwnerId { get; set; }   
    public Person  Owner { get; set; }

    /// <summary>
    ///     Can be used to identify the List of Donors   also besides linking donations to fundraiser. 
    /// </summary>
    public ICollection<Donation> Donations { get; set; }



}