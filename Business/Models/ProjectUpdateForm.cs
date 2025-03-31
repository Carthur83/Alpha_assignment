namespace Business.Models;

public class ProjectUpdateForm
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public int ClientId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public int StatusId { get; set; }
}
