public class UpdateRecordException : Exception
{
    public UpdateRecordException() : base() { }
    public UpdateRecordException(string message) : base(message) { }
    public UpdateRecordException(string message, Exception innerException)
        : base(message, innerException) { }
}