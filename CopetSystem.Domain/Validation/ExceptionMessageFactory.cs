using System;
namespace CopetSystem.Domain.Validation
{
	public class ExceptionMessageFactory
	{
		public static string MinLength(string prop, int length) => $"Valor do campo {prop} muito curto, o mínimo de caracteres é {length}.";
        public static string MaxLength(string prop, int length) => $"Valor do campo {prop} muito grande, o máximo de caracteres é {length}.";
        public static string WithLength(string prop, int length) => $"Valor do campo {prop} inválido, o número de caracteres deve ser {length}.";
        public static string Required(string prop) => $"Valor do campo {prop} inválido. {FirstCharToUpper(prop)} precisa ser preenchido.";
        public static string Invalid(string prop) => $"Valor do campo {FirstCharToUpper(prop)} inválido.";
        public static string InvalidEmail(string prop) => $"Valor do campo {FirstCharToUpper(prop)} inválido. O e-mail informado não é válido.";
        public static string InvalidCpf() => $"Valor de CPF inválido. Sequência de números não é um CPF válido pela lógica do governo.";

        private static string FirstCharToUpper(string input) => $"{input.FirstOrDefault().ToString().ToUpper()}{input.Substring(1)}";
    }
}

