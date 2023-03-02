using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using PetShelter.Domain;
using System.Collections.Immutable;
using PetShelter.Domain.Services;
using FluentValidation;

namespace PetShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundraiserController : ControllerBase
    {
        private readonly IFundraiserService _fundraiserService;

        public FundraiserController(IFundraiserService fundraiserService)
        {
            this._fundraiserService = fundraiserService;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IdentifiablePet>> Get(int id)
        //{
        //    var pet = await this._petService.GetPet(id);
        //    if (pet is null)
        //    {
        //        return this.NotFound();
        //    }

        //    return this.Ok(pet.AsResource());
        //}

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IReadOnlyList<IdentifiablePet>>> GetPets()
        //{
        //    var data = await this._petService.GetAllPets();
        //    return this.Ok(data.Select(p => p.AsResource()).ToImmutableArray());
        //}

        //[HttpOptions]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult Options()
        //{
        //    this.Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
        //    return this.Ok();
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<IActionResult> RescuePet([FromBody] RescuedPet pet)
        //{
        //    var id = await _petService.RescuePetAsync(pet.Rescuer.AsDomainModel(), pet.AsDomainModel());
        //    return CreatedAtRoute(nameof(RescuePet), id);
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> UpdatePet(int id, [FromBody] Resources.Pet pet)
        //{
        //    await this._petService.UpdatePetAsync(id, pet.AsPetInfo());

        //    return this.NoContent();
        //}


        [HttpPost("createFundraiser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateFundraiser( int ownerId, [FromBody] Resources.Fundraiser fundraiser)
        {
            await _fundraiserService.CreateFundraiserAsync(fundraiser.AsDomainModel(), ownerId);
            return this.Ok(); 
        }




    }
}