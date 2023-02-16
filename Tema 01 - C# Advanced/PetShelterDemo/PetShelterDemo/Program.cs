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

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;
using System.ComponentModel;

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

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}


void RegisterFundraiser()
{
    var name = ReadString("Title?");
    var description = ReadString("Description?");

    Console.WriteLine("Select the total sum of donation. The target !  Please specify the ammount and  currency (EUR,USD,RON). Example: 100 RON");
    var userAmmountInput = ReadString();
    int donationTarget = Int32.Parse(userAmmountInput.Split(" ")[0]);
    string currencyTarget = userAmmountInput.Split(" ")[1];

    var  fundraiser = new Fundraiser(name, description, currencyTarget, donationTarget);

    shelter.RegisterFundraiser( fundraiser );
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate? Please specify the ammound and  currency: RON EUR or USD. Example: 100 RON");
    var userAmmountInput = ReadString();
    int ammount = Int32.Parse(userAmmountInput.Split(" ")[0]);
    string currency = userAmmountInput.Split(" ")[1];
   
    shelter.Donate(person, ammount, currency);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonations()}");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void SeeFundraisers()
{

    var fundraisers = shelter.GetAllFundraisers();

    var fundraiserOptions = new Dictionary<string, Action>();
    foreach (var fundraiser in fundraisers)
    {
        fundraiserOptions.Add(fundraiser.Name, () => SeeFundraiserDetailsByName(fundraiser.Name));
    }

    PresentOptions("Fundraisers to choose from: ", fundraiserOptions);
}


void SeeFundraiserDetailsByName(string name)
{
    var fundraiser = shelter.GetFundraiserByName(name);
    Console.WriteLine($"About {fundraiser.Name}: {fundraiser.Description}, Donation Target: {fundraiser.DonationTarget},{fundraiser.DonationCurrency}, At the moment: {fundraiser.GetTotalDonations()}");
    Console.WriteLine($"Total ammount in target currency: {fundraiser.GetConvertedAmmountInTargetCurrency()}"); 
    Console.WriteLine(); Console.WriteLine("Current donators: ");
    foreach (var donor in fundraiser.GetAllDonors())
    {
        Console.WriteLine(donor.Name);
    }

      PresentOptions( "Would you like to donate ?",
           new Dictionary<string, Action>
           {
                { "yes", () => DonateToFundraiser(fundraiser)},
                { "No, thank you.", () =>  {} }
           }
       ); 
}

void DonateToFundraiser(Fundraiser fundraiser)
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate? Please specify the ammound and  currency: RON EUR or USD. Example: 100 RON");
    var userAmmountInput = ReadString();
    int ammount = Int32.Parse(userAmmountInput.Split(" ")[0]);
    string currency = userAmmountInput.Split(" ")[1];

    fundraiser.Donate(person, ammount, currency);
}  




void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
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