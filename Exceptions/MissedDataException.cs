namespace API_REST.Exceptions
{
    public class MissedDataException : Exception
    {
        public MissedDataException()
        {
        }

        public MissedDataException(string message)
            : base(message)
        {
        }

    }
}
