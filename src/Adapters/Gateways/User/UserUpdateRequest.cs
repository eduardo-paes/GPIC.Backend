﻿using Adapters.Gateways.Base;
using Domain.UseCases.Ports.User;

namespace Adapters.Gateways.User
{
    public class UserUpdateRequest : UserUpdateInput, IRequest { }
}