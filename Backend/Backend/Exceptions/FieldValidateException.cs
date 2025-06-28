namespace Backend.Exceptions
{
    public class FieldValidateException : Exception
    {
        public string FieldName { get; set; }

        public FieldValidateException(string fieldName, string? message) : base(message)
        {
            FieldName = fieldName;
        }
    }
}
