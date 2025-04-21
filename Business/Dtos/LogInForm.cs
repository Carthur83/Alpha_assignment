using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class LogInForm
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

