using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum EStudentAssistanceProgram
    {
        [Description("O aluno possui bolsa de assistência estudantil?")]
        HasScholarship,

        [Description("Programa de Auxílio ao Estudante com Deficiência (PAED)")]
        PAED,

        [Description("Programa de Auxílio Emergencial (PAEm)")]
        PAEm,

        [Description("Programa de Auxílio ao Estudante (PAE)")]
        PAE,

        [Description("Auxílio Digital")]
        DigitalAid,

        [Description("Outra")]
        Other,

        [Description("Não possui bolsa de assistência estudantil")]
        NoScholarship
    }
}