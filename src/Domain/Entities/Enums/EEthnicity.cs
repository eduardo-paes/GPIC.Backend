using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum ERace
    {
        [Description("Amarela")]
        Yellow,

        [Description("Branca")]
        White,

        [Description("Indígena")]
        Indigenous,

        [Description("Não declarado")]
        NotDeclared,

        [Description("Não dispõe da informação")]
        NoInformationAvailable,

        [Description("Parda")]
        Brown,

        [Description("Preta")]
        Black
    }
}