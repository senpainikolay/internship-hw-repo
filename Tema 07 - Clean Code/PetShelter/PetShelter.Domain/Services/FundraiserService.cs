using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Exceptions;
using PetShelter.Domain.Extensions.DataAccess;
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
            var person = await _personRepository.GetById(ownerId);
            if (person == null)
            {
                throw new NotFoundException($"Owner with id {ownerId} not found for the fundraiser.");
            }
            await _fundraiserRepository.Add(fundraiser.FromDomainModel(person)); 

        }
        public async Task<string?> DonateToFundraiserAsync(Donation donation) 
        {
            var person = await _personRepository.GetById(donation.DonorId);
            if (person == null)
            {
                throw new NotFoundException($"Owner with id {donation.DonorId} not found for the fundraiser."); 
            }
            var fundraiser = await _fundraiserRepository.GetById(donation.FundraiserId);

            if (fundraiser == null)
            {
                throw new NotFoundException($"Fundraiser with id {donation.FundraiserId} not found for the fundraiser.");
            }

            if (fundraiser.CurrentAmount >= fundraiser.GoalAmount || fundraiser.Status == "Closed" ) 
            {
                return $" Fundraiser with id {donation.FundraiserId} either reached the due date or reached its target! Look into other Fundraiser!" ;
            }
            fundraiser.CurrentAmount += donation.Amount;
            if (fundraiser.CurrentAmount >= fundraiser.GoalAmount || DateTime.Now >= fundraiser.DueDate ) { fundraiser.Status = "Closed"; await _fundraiserRepository.Update(fundraiser); }

           await _donationRepository.Add(donation.FromDomainModel(person,fundraiser));  


           return null;

        }

        public async Task<ICollection<Fundraiser?>> GetAllFundraisers()
        {
            var fundraisers = await _fundraiserRepository.GetAll();
            return fundraisers.Select(f => f.ToDomainModel()).ToList();
        }


        public async Task DeleteFundaiser(int fundraiserId)
        {  
            var fundraiser = await _fundraiserRepository.GetById(fundraiserId); 
            if (fundraiser == null)
            {
                throw new NotFoundException($"Fundraiser with id {fundraiserId} not found for deletion/closing!!");

            }
            await _fundraiserRepository.DeleteFundraiser(fundraiser); 
            
        }

        public async Task<Fundraiser?> GetFundraiser(int fundraiserId)
        {
            var fundraiser = await _fundraiserRepository.GetById(fundraiserId);
            if (fundraiser == null)
            {
                return null;
            }
            var domainFundraiser = fundraiser.ToDomainModel();

            var donors = await _fundraiserRepository.GetDonorsForFundraiserId(fundraiserId); 

            if (donors == null)
            {
                return domainFundraiser;

            } else
            {
                domainFundraiser.Donors = donors.Select(d => d.ToDomainModel()).ToImmutableArray(); 
                return domainFundraiser;
            }

        }

     
    }
}
