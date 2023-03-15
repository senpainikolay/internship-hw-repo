using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain;
using System.Collections.Immutable;
using PetShelter.Domain.Services;
using Microsoft.AspNetCore.Cors;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAll")]
    public class FundraiserController : ControllerBase
    {
        private readonly IFundraiserService _fundraiserService;

        public FundraiserController(IFundraiserService fundraiserService)
        {
            this._fundraiserService = fundraiserService;
        }

        
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<FundraiserMinimalInfo>>> GetFundraisers()
        {
            var data = await this._fundraiserService.GetAllFundraisers();
            return this.Ok(data.Select(p => p.AsMinimalResource()).ToImmutableArray());
        }

    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FundraiserWithDonors>> Get(int id)
        {
            var fundraiser = await _fundraiserService.GetFundraiser(id); 

            if (fundraiser is null)
            {
                return this.NotFound();
            }

            return this.Ok(fundraiser.AsResource());
        }

        [HttpDelete("deleteFundraiser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteFundraiser(int id)
        {
            await _fundraiserService.DeleteFundaiser(id); 
            return this.Ok();
        }


        [HttpPost("createFundraiser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFundraiser(int OwnerId, [FromBody] Resources.Fundraiser fundraiser)
        {
            await _fundraiserService.CreateFundraiserAsync(fundraiser.AsDomainModel(), OwnerId);
            return this.Ok(); 
        }

    

        [HttpPost("donateToFundraiser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> DonateToFundraiser([FromBody] Resources.Donation donation)
        {
            var res = await _fundraiserService.DonateToFundraiserAsync(donation.AsDomainModel());
            if (res == null) 
            { return this.Ok(); 
            } else { 
                return this.StatusCode(406, res); 
            }

        }




    }
}