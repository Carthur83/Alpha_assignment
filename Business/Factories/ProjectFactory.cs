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
        };
    }

    public static ProjectEntity CreateEntity(Project project)
    {
        return new ProjectEntity
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            ClientId = project.ClientId,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            StatusId = project.StatusId
        };
    }

    public static Project CreateModel(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            ClientId = entity.ClientId,
            ClientName = entity.Client.ClientName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            StatusId = entity.StatusId,
            Status = entity.Status.Status.ToLower()
        };
    }

    public static ProjectEntity CreateEntity(ProjectUpdateForm form)
    {
        return new ProjectEntity
        {
            Id = form.Id,
            ProjectName = form.ProjectName,
            ClientId = form.ClientId,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Budget = form.Budget,
            StatusId = form.StatusId
        };
    }
}
