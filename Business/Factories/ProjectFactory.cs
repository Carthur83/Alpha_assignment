using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity CreateEntity(AddProject project)
    {
        return new ProjectEntity
        {
            ProjectName = project.ProjectName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            Status = project.Status
        };
    }

    public static Project CreateModel(ProjectEntity entity)
    {
        return new Project
        {
            ProjectName = entity.ProjectName,
            ClientName = entity.Client.ClientName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Status = entity.Status
        };
    }
}
