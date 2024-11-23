using System.ComponentModel.DataAnnotations;

namespace Readings_Guide.Cores.Dtos
{
    public class RegisterModelDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
