using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain.Services;


public interface IFundraiserService
{
    Task CreateFundraiserAsync(Fundraiser fundraiser, int ownerId);
    Task<string?> DonateToFundraiserAsync(Donation donation);
    Task<ICollection<Fundraiser?>> GetAllFundraisers();

    Task<Fundraiser?> GetFundraiser(int fundraiserId);
    Task DeleteFundaiser(int fundraiserId); 



}
