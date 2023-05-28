namespace Domain.Validation;
public class EntityExceptionValidation : Exception
{
    public EntityExceptionValidation(string error) : base(error) { }
    public EntityExceptionValidation() : base() { }
    public EntityExceptionValidation(string? message, Exception? innerException) : base(message, innerException) { }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new EntityExceptionValidation(error);
    }
}
