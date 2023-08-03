using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.UseCases.Interfaces.Student;
using Domain.UseCases.Ports.Student;
using Domain.Validation;

namespace Domain.UseCases.Interactors.Student
{
    public class GetStudentByRegistrationCode : IGetStudentByRegistrationCode
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GetStudentByRegistrationCode(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<DetailedReadStudentOutput> ExecuteAsync(string? registrationCode)
        {
            // Verifica de a matrícula do aluno foi informada
            UseCaseException.NotInformedParam(string.IsNullOrEmpty(registrationCode), "Matrícula");

            // Busca o aluno pelo código de matrícula
            var student = await _studentRepository.GetByRegistrationCodeAsync(registrationCode!);

            // Verifica se o aluno foi encontrado
            UseCaseException.NotFoundEntityByParams(student is null, nameof(Entities.Student));

            // Mapeia o aluno para a saída detalhada
            return _mapper.Map<DetailedReadStudentOutput>(student);
        }
    }
}