using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IClientService clientService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IClientService _clientService = clientService;

    public async Task<bool> AddProject(AddProject project)
    {
        if (project == null)
            return false;

        var client = await _clientService.GetClientAsync(x => x.ClientName == project.ClientName);

        if (client == null)
        {
            client = await _clientService.CreateClientAsync(project.ClientName);
            if (client == null)
                return false;
        }

        var projectEntity = ProjectFactory.CreateEntity(project);
        projectEntity.ClientId = client.Id;

        await _projectRepository.CreateAsync(projectEntity);
        return true;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.CreateModel);

        return projects;
    }

    public async Task<Project?> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        if (entity == null)
            return null;

        return ProjectFactory.CreateModel(entity);
    }


}
