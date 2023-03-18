using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests
{
    public class RescuePetTests
    {
        private readonly IPersonService _personService;
        private readonly PetService _petServiceSut;

        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly Mock<IIdNumberValidator> _mockIdNumberValidator;

        private RescuePetRequest _request;

        public readonly static IEnumerable<object[]> InvalidPetPersonDatesOfBirth = new List<object[]>{

              new object[] { DateTime.Now},
              new object[] { DateTime.Now.AddYears(-5) },
               new object[] {DateTime.Now.AddYears(-17).AddMonths(-11).AddDays(-27) }
        };


        public RescuePetTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockPetRepository = new Mock<IPetRepository>();
            _mockIdNumberValidator = new Mock<IIdNumberValidator>();

            _personService = new PersonService(_mockPersonRepository.Object, _mockIdNumberValidator.Object, new PersonValidator());
            _petServiceSut = new PetService(_personService, _mockPetRepository.Object, new RescuePetRequestValidator(), new AdoptPetRequestValidator());
        }


        private void SetupHappyPath()
        {
            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(true);

            _request = new RescuePetRequest
            {
                PetName = "Max",
                Type = Constants.PetType.Dog,
                Description = "Nice dog",
                IsHealthy = true,
                ImageUrl = "test",
                WeightInKg = 10,
                Person = new BusinessLayer.Models.Person
                {
                    DateOfBirth = DateTime.Now.AddYears(-Constants.PersonConstants.AdultMinAge - 2),
                    IdNumber = "1111222233334",
                    Name = "TestName"
                }
            };
        }

        [Fact]
        public async void GivenValidData_WhenRescuePet_PetIsAdded()
        {
            //Arrange
            SetupHappyPath();

            //Act
            await _petServiceSut.RescuePet(_request);

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Once);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(-5)]
        [InlineData(0)]
        public async Task GiventWeightIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(decimal weight)
        {
            // Arrange
            SetupHappyPath();
            _request.WeightInKg = weight;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Fact]
        public async Task GivenIdNumberIsInvalid_WhenRescuePet_ThenThowsArgumentException()
        {
            //Arrange
            SetupHappyPath();

            _mockIdNumberValidator.Setup(x => x.Validate(It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            exception.Message.Should().Be("CNP format is invalid");

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPetNameIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.PetName = "";

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(5)]
        public async Task GiventPetTypeIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(int enumPetType)
        {
            // Arrange
            SetupHappyPath();
            _request.Type = (PetType)enumPetType;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPetPersonIsEmpty_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.Person = new BusinessLayer.Models.Person();

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }

        [Fact]
        public async Task GivenPetPersonNameIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded()
        {
            // Arrange
            SetupHappyPath();
            _request.Person.Name = "P";

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));

            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }



        //
        // Added an actual Validator for  Person Age (Datetime) in order for tests to not  fail.
        //

        [Theory]
        [MemberData(nameof(InvalidPetPersonDatesOfBirth))]
        public async Task GiventPetPersonDateOfBirthIsInvalid_WhenRescuePet_ThenThrowsArgumentException_And_PetIsNotAdded(DateTime dateOfBirth)
        {
            // Arrange
            SetupHappyPath();
            _request.Person.DateOfBirth = dateOfBirth;

            //Act
            await Assert.ThrowsAsync<ArgumentException>(() => _petServiceSut.RescuePet(_request));


            //Assert
            _mockPetRepository.Verify(x => x.Add(It.Is<Pet>(p => p.Name == _request.PetName)), Times.Never);
        }





















    }
}