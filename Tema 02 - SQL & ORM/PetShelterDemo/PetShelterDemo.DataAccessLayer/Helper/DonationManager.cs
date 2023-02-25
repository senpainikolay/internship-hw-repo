

using PetShelterDemo.DataAccessLayer.Models;

namespace PetShelterDemo.DataAccessLayer.Helper;

public static class DonationManager
{
    public static ICollection<string> AvailableCurrencies { get; set; } =
        new List<string> { "RON", "EUR", "USD", };

    // Hard coded, we could implemnt a method to update ConvertTable daily     EXAMPLE: { 1  RON } -> [1RON, 4.90EUR, 4.57USD]   
    public static Dictionary<string, double[]> ConvertTable { get; } =
        new Dictionary<string, double[]>() { { "RON", new[] { 1, 4.90, 4.57 } }, { "EUR", new[] { 0.20, 1, 0.93 } }, { "USD", new[] { 0.22, 1.07, 1 } } };



    public static void CheckCurrencyValidation(string currency)
    {
        currency = currency.ToUpper(); 
        bool isValid = false;
        foreach (var currencyType in AvailableCurrencies)
        {
            if (currency == currencyType) { isValid = true; break; }
        }

        if (!isValid) throw new Exception("Invalid Currency Input");


    }

    public static Dictionary<string, int> ConvertDonationListToDictionary(ICollection<Donation> donationList)
    {
        Dictionary<string, int> DonationsSumDict = AvailableCurrencies.ToDictionary(keySelector: c => c, elementSelector: el => 0);

        foreach (var donation in donationList)
        {
            DonationsSumDict[donation.Currency] += donation.Ammount;
        }
        return DonationsSumDict;
    }


    public static string GetTotalDonations(ICollection<Donation>? donationList)
    {
        if (donationList.Count == 0 ) return "No donations yet...";

        var DonationsSumDict = ConvertDonationListToDictionary(donationList);

        return string.Join(",", DonationsSumDict.Select(elem => elem.Key + "=" + elem.Value).ToArray());
    }


   
}