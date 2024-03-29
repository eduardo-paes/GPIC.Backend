﻿using Domain.Entities;
using Domain.Interfaces.Repositories.Bases;

namespace Domain.Interfaces.Repositories
{
    public interface IStudentRepository : IGenericCRUDRepository<Student>
    {
        /// <summary>
        /// Busca aluno pelo código de matrícula
        /// </summary>
        /// <param name="registrationCode">Código de matrícula</param>
        /// <returns>Aluno encontrado</returns>
        Task<Student?> GetByRegistrationCodeAsync(string registrationCode);

        /// <summary>
        /// Obtém estudante pelo Id do usuário informado.
        /// </summary>
        /// <param name="userId">Id do usuário.</param>
        /// <returns>Estudante encontrado.</returns>
        Task<Student?> GetByUserIdAsync(Guid? userId);
    }
}