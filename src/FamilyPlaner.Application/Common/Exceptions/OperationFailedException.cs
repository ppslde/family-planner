namespace FamilyPlaner.Application.Common.Exceptions;

public class OperationFailedException : Exception
{
    public OperationFailedException(string message, params string[] errors)
      : base(message)
    {
        Errors = errors;
    }

    public IEnumerable<string> Errors { get; }
}
