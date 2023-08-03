namespace Domain.Validation;
public class UseCaseException : Exception
{
    public UseCaseException(string error) : base(error) { }
    public UseCaseException() : base() { }
    public UseCaseException(string? message, Exception? innerException) : base(message, innerException) { }

    public static Exception BusinessRuleViolation(string message) => new UseCaseException(message);
    public static Exception NotFoundEntityById(string entityName) => new UseCaseException($"Entidade ({entityName}) não encontrada através do Id informado.");
    public static Exception NotFoundEntityByParams(string entityName) => new UseCaseException($"Entidade ({entityName}) não encontrada através dos parâmetros informados.");

    public static void NotFoundEntityByParams(bool hasError, string entityName)
    {
        if (hasError) throw NotFoundEntityByParams(entityName);
    }

    public static void NotFoundEntityById(bool hasError, string entityName)
    {
        if (hasError) throw NotFoundEntityById(entityName);
    }

    public static void BusinessRuleViolation(bool hasError, string message)
    {
        if (hasError) throw BusinessRuleViolation(message);
    }

    public static void NotInformedParam(bool hasError, string paramName)
    {
        if (hasError) throw new UseCaseException($"Parâmetro ({paramName}) é obrigatório.");
    }
}
