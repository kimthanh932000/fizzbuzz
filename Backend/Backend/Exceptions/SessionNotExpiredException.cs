namespace Backend.Exceptions
{
    public class SessionNotExpiredException : Exception
    {
        public SessionNotExpiredException()
            : base("Session is still active. Scores can only be viewed after the session expires.") { }
    }
}