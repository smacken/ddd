namespace DomainDriven
{
    public class DomainException : Exception
    {
        public DomainException(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}