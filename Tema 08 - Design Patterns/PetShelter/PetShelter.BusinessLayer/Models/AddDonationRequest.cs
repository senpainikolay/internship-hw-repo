namespace PetShelter.BusinessLayer.Models;

public class AddDonationRequest
{
    public decimal Amount { get; set; }
    public DataAccessLayer.Models.Person Donor { get; set; }

}