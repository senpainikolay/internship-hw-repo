//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// } 
///

using PetShelterDemo.DataAccessLayer.Models;
using PetShelterDemo.DataAccessLayer.Helper;
using PetShelterDemo.Domain;


var shelter = new PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!"); 


var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                 { "Register a Fundraiser", RegisterFundraiser },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                 { "See fundraisers", SeeFundraisers },
                { "Break our database connection", BreakDatabaseConnection },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}


void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new CustomPet().WithNameAndDesription(name, description).Build();

    shelter.RegisterPet(pet);
}


void RegisterFundraiser()
{
    var name = ReadString("Title?");
    var description = ReadString("Description?");

    Console.WriteLine(" Please specify the target ammount and currency (EUR,USD,RON). Example: 100 RON"); 

    var userAmmountInput = ReadString();

    int ammount = 0;
    string currency = "";
    try
    {
        ammount = int.Parse(userAmmountInput.Split(" ")[0]);
        currency = userAmmountInput.Split(" ")[1]; 
        DonationManager.CheckCurrencyValidation(currency);
        var fundraiser = new Fundraiser(name, description, ammount, currency);
        shelter.RegisterFundraiser(fundraiser);

    }
    catch (Exception)
    {
        PresentOptions("Please specify correct data. Something wrong with data input",
            new Dictionary<string, Action>
            {
                { "Go back.", () =>  {} }
            }
        );
    }
          
}

void Donate()
{
    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var idNumber = ReadString();

    var donor = shelter.GetPersonById(idNumber).Result;
    if (donor == null)
    {
        Console.WriteLine("Seems you are not registered. What is your name ?");
        var name = ReadString();
        var p = new Person(name, idNumber);
        donor = p; 
    } 

    Console.WriteLine("How much would you like to donate? Please specify the ammount and  currency: RON EUR or USD. Example: 100 RON");
    var userAmmountInput = ReadString(); 

    int ammount = 0;
    string currency = "";
    try
    {
        ammount = int.Parse(userAmmountInput.Split(" ")[0]);
        currency = userAmmountInput.Split(" ")[1];
        DonationManager.CheckCurrencyValidation(currency);
        var donation = new CustomDonation().WithAmmountAndCurrency(ammount, currency).ForShelter(donor).Build();
        shelter.Donate(donor, donation);

    }
    catch (Exception)
    {
        PresentOptions("Please specify correct data. Something wrong with data input",
            new Dictionary<string, Action>
            {
                { "Go back.", () =>  {} }
            }
        );
    }

   
}

  void SeeDonations()
{
    Console.WriteLine($"Our current donation total is { shelter.GetTotalDonations().Result}");
    Console.WriteLine("Special thanks to our donors:");
    var donors =  shelter.GetAllDonors().Result;
    foreach (var donor in donors)
    {
      Console.WriteLine(donor.Name);
      
    } 

}

void  SeePets()
{

    var pets =   shelter.GetAllPets().Result;
    if (pets.Count != 0) {

        var petOptions = new Dictionary<string, Action>();
        foreach (var pet in pets)
        {
            petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
        }

        PresentOptions("We got..", petOptions);
    } else
    {
        Console.WriteLine("No pets to show... \n");

    }
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name).Result;
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void SeeFundraisers()
{  


    var fundraisers = shelter.GetAllFundraisers().Result;
    if (fundraisers.Count != 0)
    {

        var fundraiserOptions = new Dictionary<string, Action>();
    foreach (var fundraiser in fundraisers)
    {
            fundraiserOptions.Add(fundraiser.Name, () => SeeFundraiserDetails(fundraiser));
    }

    PresentOptions("Fundraisers to choose from: ", fundraiserOptions);

} else
{
    Console.WriteLine("No fundraisers registered yet... \n");

}
}


void SeeFundraiserDetails(Fundraiser fundraiser)
{

    var currentFundraiserDonationAmmount = shelter.GetConvertedTotalSumOfFundraiser(fundraiser.DonationCurrency, fundraiser.Id).Result; 

    Console.WriteLine($"About {fundraiser.Name}. The scope: {fundraiser.Description}. \n Donation Target: {fundraiser.DonationTarget},{fundraiser.DonationCurrency}, At the moment: {DonationManager.GetTotalDonations(fundraiser.Donations)}");
    Console.WriteLine($"Total ammount in target currency: {currentFundraiserDonationAmmount} {fundraiser.DonationCurrency} ");
    Console.WriteLine(); Console.WriteLine("Current donors: \n");

    foreach (var don in fundraiser.Donations)
    {
        var per = shelter.GetPersonDetailsById(don.DonorId).Result;
        Console.WriteLine(per.Name);
    }

    if (currentFundraiserDonationAmmount >= fundraiser.DonationTarget)
    {
        PresentOptions($"The fundraiser reached its Donation Target! Please take a look at other fundraisers!",
          new Dictionary<string, Action>
          {
                { "Ok. Thank you! ", () =>  {} }
          }
      );

    }
    else
    {
        PresentOptions($"Would you like to donate ?",
             new Dictionary<string, Action>
             {
                { "yes", () => DonateToFundraiser(fundraiser)},
                { "No, thank you.", () =>  {} }
             }
         );
    }
}



void DonateToFundraiser(Fundraiser fundraiser)
{
    Console.WriteLine("What's your personal Id?");
    var id = ReadString();

    var donor = shelter.GetPersonById(id).Result; 
    if (donor == null)
    {
        Console.WriteLine("Seems you are not registered. What is your name ?");
        var name = ReadString();
        var p = new Person(name, id);
        donor = p; 
    }


    Console.WriteLine("How much would you like to donate? Please specify the ammount and  currency: RON EUR or USD. Example: 100 RON");
    var userAmmountInput = ReadString();

    int ammount = 0;
    string currency = "";
    try
    {
        ammount = int.Parse(userAmmountInput.Split(" ")[0]);
        currency = userAmmountInput.Split(" ")[1];
        DonationManager.CheckCurrencyValidation(currency);
        var donation = new CustomDonation().WithAmmountAndCurrency(ammount, currency).ForFundraiser(fundraiser, donor).Build();
        shelter.Donate(donor, donation);

    }
    catch (Exception)
    {
        PresentOptions("Please specify correct data. Something wrong with data input",
            new Dictionary<string, Action>
            {
                { "Go back.", () =>  {} }
            }
        );
    } 


}




void BreakDatabaseConnection()
{
     Console.WriteLine(" No command for breaking Databse ");
   // Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}

int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}