namespace Domain.Validation;
public static class ExceptionMessageFactory
{
    public static string MinLength(string prop, int length) => $"O valor de ({prop}) é muito curto. O comprimento mínimo é de {length} caracteres.";
    public static string MaxLength(string prop, int length) => $"O valor de ({prop}) é muito longo. O comprimento máximo é de {length} caracteres.";
    public static string WithLength(string prop, int length) => $"Valor inválido para ({prop}). O número de caracteres deve ser {length}.";
    public static string Required(string prop) => $"Valor inválido para ({prop}). {prop} deve ser fornecido.";
    public static string Invalid(string prop) => $"Valor inválido para ({prop}).";
    public static string InvalidEmail(string prop) => $"Valor inválido para ({prop}). O email fornecido não é válido.";
    public static string InvalidCpf() => "Valor de CPF inválido. A sequência numérica não é um CPF válido de acordo com a lógica do governo.";
    public static string LessThan(string prop, string value) => $"O valor de ({prop}) não pode ser menor que {value}.";
    public static string BusinessRuleViolation(string message) => message;
}