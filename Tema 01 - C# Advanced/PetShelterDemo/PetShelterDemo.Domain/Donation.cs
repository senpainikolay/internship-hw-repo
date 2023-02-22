

namespace PetShelterDemo.Domain;



public class Donations
{

    public Dictionary<string, int> DonationsDict;
    // Hard coded, we could implemnt a method to update ConvertTable daily     EXAMPLE: { 1  RON } -> [1RON, 4.90EUR, 4.57USD]   
    public static Dictionary<string, double[]> ConvertTable { get;  } = 
        new Dictionary<string, double[]>() { { "RON", new[] { 1, 4.90, 4.57 } }, { "EUR", new[] { 0.20, 1, 0.93 } }, { "USD", new[]{ 0.22, 1.07, 1 } } };



public Donations()
    {
        DonationsDict = new Dictionary<string, int>() { { "RON", 0 }, { "EUR", 0 }, { "USD", 0 } };
    } 

    public void RegisterDonation(int ammount, string currency) 
    {
        currency = currency.ToUpper();
        if (!DonationsDict.ContainsKey(currency)) throw new Exception($"Invalid currency: {currency}"); 
        
        DonationsDict[currency] += ammount;
    }

    public string GetTotalDonations()
    {
        return string.Join(",", DonationsDict.Select(elem => elem.Key + "=" + elem.Value).ToArray());
    }  

    public  double ConvertDonationsToCurrency(string targetCurrency)
    {
        int[]  totalAmmountArr = DonationsDict.Select(elem => elem.Value).ToArray();
        double[] currentCurrencyValues = ConvertTable[targetCurrency]; 

        var sum = 0.0; 
        for (int i = 0; i < totalAmmountArr.Length; i++)
        {
           var num1 = totalAmmountArr[i]; 
           var num2 = currentCurrencyValues[i];
            sum += num1 * num2;
        }

        return sum;
    }
}