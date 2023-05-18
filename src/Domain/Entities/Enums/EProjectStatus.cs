using System.ComponentModel;

namespace Domain.Entities.Enums
{
    public enum EProjectStatus
    {
        [Description("Aberto: neste status os dados do projeto devem ser completamente preenchidos para ser Submetido.")]
        Open,

        [Description("Avaliação: neste status o projeto foi Submetido e se encontra disponível para avaliação por parte do Avaliador.")]
        Evaluation,

        [Description("Indeferido: quando o avaliador Reprova um projeto, esse passa para o status de Indeferido e pode passar pelo processo de Ressubmissão no período de RECURSO, sem alteração do projeto.")]
        Rejected,

        [Description("Deferido: neste status o projeto está pronto para receber os documentos necessários para ser iniciado - os documentos devem ser entregues dentro de um período de XX dias.")]
        Accepted,

        [Description("Análise: neste status o projeto já recebeu toda a documentação necessária e essa se encontra em análise pelo Avaliador.")]
        DocumentAnalysis,

        [Description("Iniciado: neste status o projeto já recebeu toda a documentação necessária e essa foi aprovada.")]
        Started,

        [Description("Pendente: neste status o projeto já recebeu toda a documentação necessária, mas essa não foi aprovada - os documentos podem ser reenviados dentro de um período de XX dias.")]
        Pending,

        [Description("Cancelado: neste status o projeto foi cancelado por algum motivo.")]
        Cancelled,

        [Description("Encerrado: neste status o projeto já foi concluído e o Orientador possui 30 dias para entregar o Relatório Final, do contrário sua conta será suspensa por XX anos.")]
        Closed
    }
}