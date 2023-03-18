
using PetShelter.BusinessLayer.Models;



namespace PetShelter.BusinessLayer.Tests;


public class CustomAddDonationRequest
{
    private readonly AddDonationRequest _customAddRequest = new AddDonationRequest();


    public AddDonationRequest Build()
    {
        return _customAddRequest;
    } 


    public CustomAddDonationRequest WithRandomValidData()
    {
        _customAddRequest.Amount = 10;
        _customAddRequest.Donor = new DataAccessLayer.Models.Person
        {
            DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge - 2),
            IdNumber = "1111222233333",
            Name = "OKlolA"
        };
        return this;
    }

    public CustomAddDonationRequest WithAmmount(int ammount)
    {
        _customAddRequest.Amount = ammount;
        return this;
    }

    public CustomAddDonationRequest WithDonorIdNumber(string  idNumber)
    {
        _customAddRequest.Donor.IdNumber = idNumber;
        return this;
    }

    public CustomAddDonationRequest WithDonorName(string name)
    {
        _customAddRequest.Donor.Name = name;
        return this;
    }

    public CustomAddDonationRequest WithDonorDateOfBirth(DateTime dateOfBirth)
    {
        _customAddRequest.Donor.DateOfBirth = dateOfBirth;
        return this;
    } 





}
