using Adapters.Gateways.Base;
using Domain.Contracts.Auth;

namespace Adapters.Gateways.Auth;
public class UserResetPasswordRequest : UserResetPasswordInput, IRequest { }