using System.Globalization;

namespace PetShelterDemo.DataAccessLayer.Models
{
    public class Person : IIdEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string IdNumber { get; set; }

        public ICollection<Donation> Donations { get; set; }
        public ICollection<Pet> RescuedPets { get; set; }

        public Person(string name, string idNumber) 
        {
            Name = name;
            IdNumber = idNumber;
            Donations = new List<Donation>();
            RescuedPets = new List<Pet>();

        }



    }
}
