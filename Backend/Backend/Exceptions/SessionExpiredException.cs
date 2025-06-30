namespace Backend.Exceptions
{
    public class SessionExpiredException : Exception
    {
        public SessionExpiredException(): base("Session has expired.") { }

        public SessionExpiredException(string message): base(message) { }
    }
}
