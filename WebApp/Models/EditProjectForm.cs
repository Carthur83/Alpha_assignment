using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class EditProjectForm
{
    public int Id { get; set; }

    [Display(Name = "Project Name", Prompt = "Project Name")]
    [Required(ErrorMessage = "Enter project name")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Client Name")]
    [Required(ErrorMessage = "Select a client")]
    public int ClientId { get; set; }

    [Display(Name = "Description", Prompt = "Type something")]
    [Required(ErrorMessage = "Enter a description")]
    public string Description { get; set; } = null!;

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Enter a date")]
    public DateTime? StartDate { get; set; }

    [Display(Name = "End Date")]
    [Required(ErrorMessage = "Enter a date")]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Budget", Prompt = "0")]
    [Required(ErrorMessage = "Enter budget")]
    public decimal Budget { get; set; }
    public int StatusId { get; set; }

    public IEnumerable<SelectListItem>? Clients { get; set; }
    public IEnumerable<SelectListItem>? Statuses { get; set; }
}
