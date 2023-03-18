using FluentAssertions;
using Moq;
using PetShelter.BusinessLayer.ExternalServices;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models; 

using PetShelter.DataAccessLayer.Repository;
using PetShelter.BusinessLayer.Models;
namespace PetShelter.BusinessLayer.Tests;

using Microsoft.EntityFrameworkCore;

using PetShelter.DataAccessLayer;

using Microsoft.Extensions.Configuration;



public class IntegrationTests
{


    [Fact]
    public async Task  GivenValidRequest_WhenSendHttpHeadToCnpValidator_ResponseIsEnsured()
    {
        // Arrange
        HttpClient _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7062");
        var requestUri = $"{_httpClient.BaseAddress}cnp/testConnection";

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Head,
            RequestUri = new Uri(requestUri)
        };

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task GivenRepositoryIsValid_WhenGetAllPets_AssertTheResultIsNotNull() 
    {
        // Given 
        var optionsBuilder = new DbContextOptionsBuilder<PetShelterContext>() 
       // couldn find a  appsetting.json parser   in time for appsetting.json. 
       .UseSqlServer(@"Server =localhost\SQLEXPRESS; Database = PetShelter2; Trusted_Connection = True; TrustServerCertificate = True;");
        var context = new PetShelterContext(optionsBuilder.Options); 
        var _petRepo = new PetRepository(context);

        // When
        var result = await _petRepo.GetAll();

        // Then
        Assert.NotNull(result);

    }



}
