using Adapters.Gateways.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.Auth
{
    public class UserLoginRequest : Request
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}