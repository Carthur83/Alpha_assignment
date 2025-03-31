using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<bool> AddProject(AddProject project);
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<Project?> UpdateAsync(ProjectUpdateForm updatedProject);
    Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression);
}