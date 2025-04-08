using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddMemberForm
{
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "First Name", Prompt = "First name")]
    [Required(ErrorMessage = "Please enter first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "First Last", Prompt = "Last name")]
    [Required(ErrorMessage = "Please enter last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter an email address ")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone", Prompt = "Enter a phone number")]
    public string? Phone { get; set; }

    [Display(Name = "Job Title", Prompt = "Enter a job title")]
    [Required(ErrorMessage = "Please enter a job title")]
    public string? JobTitle { get; set; }

    [Display(Name = "Street", Prompt = "Enter a street")]
    public string? Street { get; set; }

    [Display(Name = "City", Prompt = "Enter a city")]
    public string? City { get; set; }

    [Display(Name = "Postal Code", Prompt = "Enter a postal code")]
    public string? PostalCode { get; set; }

    [Display(Name = "Date Of Birth", Prompt = "Day")]
    public int Day { get; set; }

    [Display(Prompt = "Month")]
    public int Month { get; set; }

    [Display(Prompt = "Year")]
    public int Year { get; set; }
    public List<SelectListItem> Days { get; } = Enumerable.Range(1, 31)
      .Select(d => new SelectListItem { Value = d.ToString(), Text = d.ToString() })
      .ToList();

    public List<SelectListItem> Months { get; } = new List<SelectListItem>
    {
        new("January", "1"),
        new("February", "2"),
        new("March", "3"),
        new("April", "4"),
        new("May", "5"),
        new("June", "6"),
        new("July", "7"),
        new("August", "8"),
        new("September", "9"),
        new("October", "10"),
        new("November", "11"),
        new("December", "12"),
    };

    public List<SelectListItem> Years { get; } = Enumerable.Range(1900, DateTime.Now.Year - 1899)
        .Reverse()
        .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() })
        .ToList();


}
