using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum EScore
    {
        [Description("Fraco")]
        Weak = 1,
        [Description("Regular")]
        Regular = 2,
        [Description("Bom")]
        Good = 3,
        [Description("Excelente")]
        Excellent = 4
    }
}