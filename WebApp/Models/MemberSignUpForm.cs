using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class MemberSignUpForm
{
    [Display(Name = "First Name", Prompt = "Your first name")]
    [Required(ErrorMessage = "Enter first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Your last name")]
    [Required(ErrorMessage = "Enter last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Enter email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Enter a password")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Invalid password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Your password do not match")]
    public string ConfirmPassword { get; set; } = null!;

    public bool TermsAndConditions { get; set; }
}
