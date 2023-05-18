using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum EQualification
    {
        [Description("Mestre")]
        Master = 1,

        [Description("Doutor")]
        Doctor = 2
    }
}