using System;

namespace Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error) { }

        public DomainExceptionValidation() : base()
        {
        }

        public DomainExceptionValidation(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
    }
}
