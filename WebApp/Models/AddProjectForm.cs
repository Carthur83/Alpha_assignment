using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AddProjectForm
{
    [Display(Name = "Project Name", Prompt = "Project Name")]
    [Required(ErrorMessage = "Enter project name")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Client Name")]
    [Required(ErrorMessage = "Enter client name")]
    public string ClientName { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Type something")]
    [Required(ErrorMessage = "Enter a description")]
    public string RichTextContent { get; set; } = null!;

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Enter a date")]
    public DateTime? StartDate { get; set; }

    [Display(Name = "End Date")]
    [Required(ErrorMessage = "Enter a date")]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Budget", Prompt = "0")]
    [Required(ErrorMessage = "Enter budget")]
    public decimal Budget { get; set; }
}

