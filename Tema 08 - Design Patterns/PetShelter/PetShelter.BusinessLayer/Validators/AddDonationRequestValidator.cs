using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models; 


namespace PetShelter.BusinessLayer.Validators;

public class AddDonationRequestValidator: AbstractValidator<AddDonationRequest>
{
	public AddDonationRequestValidator()
	{
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Donor.DateOfBirth).LessThan(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge));
        RuleFor(x => x.Donor.IdNumber).NotEmpty().Length(PersonConstants.IdNumberLength);
        RuleFor(x => x.Donor.Name).NotEmpty().MinimumLength(PersonConstants.NameMinLength);


    }
}
