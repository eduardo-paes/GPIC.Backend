using System.ComponentModel;
using System.Reflection;

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
}