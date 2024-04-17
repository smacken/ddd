
namespace DomainDriven
{
    public class DomainException: Exception
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public DomainException(string message)
        {
            _message = message;
        }
    }
}
