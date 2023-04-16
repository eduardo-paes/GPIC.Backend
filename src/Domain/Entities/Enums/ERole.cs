using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum ERole
    {
        [Description("Administrador")]
        ADMIN,
        [Description("Professor")]
        STUDENT
    }
}