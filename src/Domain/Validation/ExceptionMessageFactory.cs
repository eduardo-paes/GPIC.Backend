namespace Domain.Validation;
public static class ExceptionMessageFactory
{
    public static string MinLength(string prop, int length) => $"The value of ({prop}) is too short. The minimum length is {length} characters.";
    public static string MaxLength(string prop, int length) => $"The value of ({prop}) is too long. The maximum length is {length} characters.";
    public static string WithLength(string prop, int length) => $"Invalid value for ({prop}). The number of characters should be {length}.";
    public static string Required(string prop) => $"Invalid value for ({prop}). {prop} must be provided.";
    public static string Invalid(string prop) => $"Invalid value for ({prop}).";
    public static string InvalidEmail(string prop) => $"Invalid value for ({prop}). The provided email is not valid.";
    public static string InvalidCpf() => "Invalid CPF value. The number sequence is not a valid CPF according to government logic.";
    public static string LessThan(string prop, string value) => $"The value of ({prop}) cannot be less than {value}.";
}