using Adapters.Gateways.Base;
using Domain.UseCases.Ports.Auth;

namespace Adapters.Gateways.Auth
{
    public class UserLoginRequest : UserLoginInput, IRequest { }
}