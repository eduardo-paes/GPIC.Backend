﻿namespace Domain.UseCases.Ports.Auth
{
    public class UserLoginOutput
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}