using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _context.Projects
            .Include(Client => Client.Client)
            .Include(Status => Status.Status)
            .ToListAsync();

        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
        {
            return null!;
        }

        var entity = await _context.Projects
            .Include(Client => Client.Client)
            .Include(Status => Status.Status)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
