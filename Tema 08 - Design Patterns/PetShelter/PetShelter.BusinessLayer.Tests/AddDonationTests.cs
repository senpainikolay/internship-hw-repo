using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models; 

using PetShelter.DataAccessLayer.Repository;
using PetShelter.BusinessLayer.Models;



namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{ 


    private readonly IPersonService _personService;
    private readonly DonationService _donationServiceSut;
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;


    public readonly static IEnumerable<object[]> InvalidPetPersonDatesOfBirth = new List<object[]>{

              new object[] { DateTime.Now},
              new object[] { DateTime.Now.AddYears(-5) },
               new object[] {DateTime.Now.AddYears(-17).AddMonths(-11).AddDays(-27) }
        };

    private AddDonationRequest _request;

    public AddDonationTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _mockDonationRepository = new Mock<IDonationRepository>();
        _mockIdNumberValidator = new Mock<IIdNumberValidator>();

        _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());
        _donationServiceSut = new DonationService(_mockDonationRepository.Object, new AddDonationRequestValidator());
    }

    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
        _request = new CustomAddDonationRequest().WithRandomValidData().Build();

        await _donationServiceSut.AddDonation(_request);

        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == _request.Amount)), Times.Once);
    }




    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    public async Task GiventAmountIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded(int donationAmount)
    {
        // Arrange
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
        _request = new CustomAddDonationRequest().WithRandomValidData().WithAmmount(donationAmount).Build();

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(p => p.Amount == _request.Amount)), Times.Never);
    }

    [Fact]
    public async Task GivenIdNumberIsInvalid_WhenAddDonation_ThenThowsArgumentException_And_DonationIsNotAdded()
    {
        //Arrange
        _request = new CustomAddDonationRequest().WithRandomValidData().WithDonorIdNumber("821214441242").Build();
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

        //Act 
         await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));



        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(p => p.Amount == _request.Amount)), Times.Never);
    }


    [Fact]
    public async Task GivenDonorNameIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded()
    {
        // Arrange
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
        _request = new CustomAddDonationRequest().WithRandomValidData().WithDonorName("P").Build();

        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));


        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(p => p.Amount == _request.Amount)), Times.Never);
    }



    [Theory]
    [MemberData(nameof(InvalidPetPersonDatesOfBirth))]
    public async Task GiventDonorDateOfBirthIsInvalid_WhenAddDonation_ThenThrowsArgumentException_And_DonationIsNotAdded(DateTime dateOfBirth)
    {
        // Arrange
        _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);
        _request = new CustomAddDonationRequest().WithRandomValidData().WithDonorDateOfBirth(dateOfBirth).Build();


        //Act
        await Assert.ThrowsAsync<ArgumentException>(() => _donationServiceSut.AddDonation(_request));

        //Assert
        _mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(p => p.Amount == _request.Amount)), Times.Never);
    }
}
