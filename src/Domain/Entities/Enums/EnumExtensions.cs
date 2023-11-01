using System.ComponentModel;
using System.Reflection;
using Domain.Validation;

namespace Domain.Entities.Enums;
public static class EnumExtensions
{
    /// <summary>
    /// Obtém a descrição do Enum.
    /// </summary>
    /// <param name="value">Enum</param>
    /// <returns>Descrição do Enum, caso haja.</returns>
    public static string GetDescription(this Enum value)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return string.Empty;

        FieldInfo? field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString();
    }

    /// <summary>
    /// Tenta converter um objeto para um tipo Enum.
    /// </summary>
    /// <param name="value">Valor a ser convertido.</param>
    /// <typeparam name="T">Tipo para o qual ser convertido.</typeparam>
    /// <returns>Objeto com tipo convertido.</returns>
    public static T TryCastEnum<T>(object? value)
    {
        try
        {
            EntityExceptionValidation.When(value is null, $"Valor não informado para o tipo Enum {typeof(T).ToString()}.");
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (enumValue.GetHashCode().Equals(value))
                {
                    return (T)Enum.Parse(typeof(T), value?.ToString()!);
                }
            }
        }
        catch (Exception)
        {
            throw new EntityExceptionValidation($"Não foi possível converter o valor {value} para o tipo {typeof(T)}.");
        }

        throw new EntityExceptionValidation($"Valor {value} fora do intervalo permitido para o tipo {typeof(T)}.");
    }
}