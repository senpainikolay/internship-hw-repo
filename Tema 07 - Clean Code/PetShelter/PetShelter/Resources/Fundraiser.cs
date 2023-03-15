namespace PetShelter.Api.Resources
{
    public class Fundraiser
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public int OwnerId { get; set; }
        public decimal GoalAmount { get; set; } 
    } 

}
