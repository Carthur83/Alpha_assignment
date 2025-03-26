using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IClientService
{
    Task<Client?> CreateClientAsync(string clientName);
    Task<Client?> GetClientAsync(Expression<Func<ClientEntity, bool>> expression);
}