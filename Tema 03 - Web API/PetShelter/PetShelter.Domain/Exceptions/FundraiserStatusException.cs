namespace PetShelter.Domain.Exceptions
{
    [Serializable]
    public class FundraiserStatusException : DomainException
    {
        public FundraiserStatusException() { }
        public FundraiserStatusException(string message) : base(message) { }
        public FundraiserStatusException(string message, Exception inner) : base(message, inner) { }
        protected FundraiserStatusException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
