namespace API_REST.Exceptions
{
    public class UserNotValidException : Exception
    {
        public UserNotValidException()
        {
        }

        public UserNotValidException(string message)
            : base(message)
        {

        }

    }
}
