using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Primitives;
using Domain.Validation;

namespace Domain.Entities
{
    public class ProjectActivity : Entity
    {
        #region Properties
        private int? _informedActivities;
        public int? InformedActivities
        {
            get => _informedActivities;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(InformedActivities)));
                    _informedActivities = value;
                }
            }
        }
        private int? _foundActivities;
        public int? FoundActivities
        {
            get => _foundActivities;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(FoundActivities)));
                    _foundActivities = value;
                }
            }
        }

        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(ProjectId)));
                    _projectId = value;
                }
            }
        }

        private Guid? _activityId;
        public Guid? ActivityId
        {
            get => _activityId;
            set
            {
                {
                    EntityExceptionValidation.When(value == null,
                        ExceptionMessageFactory.Required(nameof(ActivityId)));
                    _activityId = value;
                }
            }
        }

        public virtual Project? Project { get; set; }
        public virtual Activity? Activity { get; set; }
        #endregion

        #region Constructors
        public ProjectActivity(Guid? projectId, Guid? activityId, int? informedActivities, int? foundActivities)
        {
            ProjectId = projectId;
            ActivityId = activityId;
            InformedActivities = informedActivities;
            FoundActivities = foundActivities;
        }

        /// <summary>
        /// Constructor to dbcontext EF instancing.
        /// </summary>
        protected ProjectActivity() { }
        #endregion

        #region Methods
        /// <summary>
        /// Calcula a pontuação da Atividade.
        /// </summary>
        /// <returns>Total obtido com o cálculo.</returns>
        public double CalculatePoints()
        {
            // Verifica se a Atividade está preenchida para calcular a pontuação.
            EntityExceptionValidation.When(Activity is null,
                ExceptionMessageFactory.BusinessRuleViolation("Atividade não informada, não é possível calcular a pontuação."));

            // A pontuação é calculada multiplicando a quantidade de atividades encontradas pela pontuação da atividade.
            double points = Activity!.Points!.Value * FoundActivities!.Value;

            // Verifica se limite foi informado, caso não tenha sido, utiliza o valor máximo do tipo.
            double limits = Activity!.Limits.HasValue ? (double)Activity.Limits.Value : double.MaxValue;

            // Se a pontuação calculada for maior que o limite da atividade, retorna o limite da atividade, caso contrário, retorna a pontuação calculada.
            return limits < points ? limits : points;
        }
        #endregion
    }
}