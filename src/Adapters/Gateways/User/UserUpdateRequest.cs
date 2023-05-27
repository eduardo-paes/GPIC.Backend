using Adapters.Gateways.Base;
using System.ComponentModel.DataAnnotations;

namespace Adapters.Gateways.User
{
    public class UserUpdateRequest : Request
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? CPF { get; set; }
    }
}