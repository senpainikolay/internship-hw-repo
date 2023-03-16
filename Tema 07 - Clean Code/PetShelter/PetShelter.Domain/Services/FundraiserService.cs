using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Exceptions;
using PetShelter.Domain.Extensions.DomainModel;
using System.Collections.Immutable;
namespace PetShelter.Domain.Services
{
    public class FundraiserService  : IFundraiserService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IFundraiserRepository _fundraiserRepository;



        public FundraiserService(IDonationRepository donationRepository, IPersonRepository personRepository, IFundraiserRepository fundraiserRepository)
        {
             _donationRepository = donationRepository;
            _personRepository = personRepository;
            _fundraiserRepository = fundraiserRepository; 
        }

        public async Task CreateFundraiserAsync(Fundraiser fundraiser, int ownerId)
        {
            var person = getExistingPerson(ownerId).Result;
            await _fundraiserRepository.Add(fundraiser.FromDomainModel(person)); 
        } 

        private async  Task<DataAccessLayer.Models.Person> getExistingPerson(int id)
        {
            var person = await _personRepository.GetById(id);
            if (person == null)
            {
                throw new NotFoundException($"Owner with id {id} not found for the fundraiser.");
            }
            return person; 
        }

        public async Task<string?> DonateToFundraiserAsync(Donation donation) 
        {
            var person = getExistingPerson(donation.DonorId).Result;
            var fundraiser = updateFundraiserStatusDetail(donation).Result;
            if (fundraiser == null)
            {
                return $" Fundraiser with id {donation.FundraiserId} either reached the due date or reached its target! Look into other Fundraiser!";
            }

            await _donationRepository.Add(donation.FromDomainModel(person,fundraiser));  


           return null;
            
        }

        private async Task<DataAccessLayer.Models.Fundraiser?> updateFundraiserStatusDetail(Donation donation )
        {
            var fundraiser = await getExistingFundraiser(donation.FundraiserId);

            if (fundraiser.CurrentAmount >= fundraiser.GoalAmount || fundraiser.Status == "Closed")
            {
                return null;
            }

            fundraiser.CurrentAmount += donation.Amount;
            if (fundraiser.CurrentAmount >= fundraiser.GoalAmount || DateTime.Now >= fundraiser.DueDate) 
            { 
                fundraiser.Status = "Closed";
                await _fundraiserRepository.Update(fundraiser); 
            }
            return fundraiser; 
        }

        private async Task<DataAccessLayer.Models.Fundraiser> getExistingFundraiser(int id)
        {
            var fundraiser = await _fundraiserRepository.GetById(id);

            if (fundraiser == null)
            {
                throw new NotFoundException($"Fundraiser with id {id} not found for the fundraiser.");
            }
            return fundraiser;
        }

        public async Task<ICollection<Fundraiser?>> GetAllFundraisers()
        {
            var fundraisers = await _fundraiserRepository.GetAll();
            return fundraisers.Select(f => f.ToDomainModel()).ToList();
        }


        public async Task DeleteFundaiser(int fundraiserId)
        {
            var fundraiser = await getExistingFundraiser(fundraiserId);

            await _fundraiserRepository.DeleteFundraiser(fundraiser); 
            
        }

        public async Task<Fundraiser?> GetFundraiser(int fundraiserId)
        {
            var fundraiser = await getExistingFundraiser(fundraiserId);

            return updateDonorsForFundraiser(fundraiser).Result; 

        } 

        private async Task<Fundraiser> updateDonorsForFundraiser(DataAccessLayer.Models.Fundraiser fundraiser)
        {
            var domainFundraiser = fundraiser.ToDomainModel();

            var donors = await _fundraiserRepository.GetDonorsForFundraiserId(fundraiser.Id);

            if (donors == null)
            {
                return domainFundraiser;

            }
            domainFundraiser.Donors = donors.Select(d => d.ToDomainModel()).ToImmutableArray();
            return domainFundraiser; 

        }




    }
}
