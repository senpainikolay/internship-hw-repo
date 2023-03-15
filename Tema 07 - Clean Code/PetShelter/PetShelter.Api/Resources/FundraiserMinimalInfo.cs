namespace PetShelter.Api.Resources
{
    public class FundraiserMinimalInfo
    {
        public string Name { get; set; } 
        public string Status { get; set; }

        public FundraiserMinimalInfo(string name, string status)
        { 

            Name = name;
            Status = status;    
              
        }
    }

 

}
