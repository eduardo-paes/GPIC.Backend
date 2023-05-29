namespace Domain.Validation;
public class UseCaseException : Exception
{
    public UseCaseException(string error) : base(error) { }
    public UseCaseException() : base() { }
    public UseCaseException(string? message, Exception? innerException) : base(message, innerException) { }

    public static Exception BusinessRuleViolation(string message) => new UseCaseException(message);
    public static Exception NotInformedParam(string paramName) => new UseCaseException($"Parameter ({paramName}) is required.");
    public static Exception NotFoundEntityById(string entityName) => new UseCaseException($"Entity ({entityName}) not found by informed id.");
    public static Exception NotFoundEntityByParams(string entityName) => new UseCaseException($"Entity ({entityName}) not found by informed parameters.");
    public static void NotInformedParam(bool hasError, string paramName)
    {
        if (hasError)
            throw new UseCaseException($"Parameter ({paramName}) is required.");
    }
}
