using Business.Models;

namespace WebApp.Models;

public class ProjectsViewModel
{
    public AddProjectForm Form { get; set; } = new();

    public IEnumerable<Project> Projects { get; set; } = [];

    public int TotalCount { get; set; }
    public int StartedCount { get; set; }
    public int CompletedCount { get; set; }


}
