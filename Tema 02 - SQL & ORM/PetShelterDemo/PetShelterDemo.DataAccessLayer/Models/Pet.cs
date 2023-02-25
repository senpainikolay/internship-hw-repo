namespace PetShelterDemo.DataAccessLayer.Models

{
    public class Pet : IIdEntity
    {   
        public int Id { get; set; }
        public string Name { get; set;  }
        public string Description { get; set; }

        public int? RescuerId { get; set; }

        public Person? Rescuer { get; set; } 

        //public Pet(string name, string description, int? rescuerId, Person? rescuer)
        //{
        //    Name = name;
        //    Description = description;
        //    RescuerId = rescuerId;
        //    Rescuer = rescuer;
        //}
    
    }

    public class CustomPet
    {
        private readonly Pet _custom = new Pet();

        public CustomPet WithNameAndDesription(string name,string description)
        {
            _custom.Name = name; 
            _custom.Description = description;
            return this;
        }

        public CustomPet WithRescuer(Person rescuer)
        {
            _custom.RescuerId = rescuer.Id;
            _custom.Rescuer = rescuer;
            return this;
        }
        public Pet Build()
        {
            return _custom;
        }
    }
}
