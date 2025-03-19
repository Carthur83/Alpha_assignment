using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UserLogInForm
{
    [Required(ErrorMessage = "Enter email")]
    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Enter a password")]
    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
