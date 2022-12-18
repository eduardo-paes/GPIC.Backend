using System;
namespace CopetSystem.Domain.Validation
{
	public class ExceptionMessageFactory
	{
		public static string MinLength(string prop, int length) => $"Invalid {prop}, too short, minimum {length} characters.";
        public static string MaxLength(string prop, int length) => $"Invalid {prop}, too big, maximun {length} characters.";
        public static string Required(string prop) => $"Invalid {prop}. {FirstCharToUpper(prop)} attribute is required.";
        public static string Invalid(string prop) => $"Invalid {FirstCharToUpper(prop)} value.";
        public static string InvalidEmail(string prop) => $"Invalid {FirstCharToUpper(prop)} value. E-mail informed isn't valid.";
        public static string InvalidCpf() => $"Invalid CPF value. Sequence of numbers isn't a valid CPF as per government logic.";

        private static string FirstCharToUpper(string input) => $"{input.FirstOrDefault().ToString().ToUpper()}{input.Substring(1)}";
    }
}

