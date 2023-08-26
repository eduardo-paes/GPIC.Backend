using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum EScholarPerformance
    {
        [Description("Ruim")]
        Bad,

        [Description("Regular")]
        Regular,

        [Description("Bom")]
        Good,

        [Description("Muito Bom")]
        VeryGood,

        [Description("Excelente")]
        Excellent
    }
}