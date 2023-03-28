using System;
using System.Threading.Tasks;
using Domain.Contracts.User;

namespace Domain.Interfaces.UseCases.User
{
    public interface IUpdateUser
    {
        Task<UserReadOutput> Execute(Guid? id, UserUpdateInput user);
    }
}