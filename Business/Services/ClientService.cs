using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<Client?> CreateClientAsync(string clientName)
    {
        if (string.IsNullOrEmpty(clientName))
        {
            return null;
        }

        var exists = await _clientRepository.ExistsAsync(x => x.ClientName == clientName);
        if (exists)
            return null;

        var entity = await _clientRepository.CreateAsync(new ClientEntity { ClientName = clientName });
        if (entity == null)
            return null;

        return ClientFactory.CreateModel(entity);
    }

    public async Task<Client?> GetClientAsync(Expression<Func<ClientEntity, bool>> expression)
    {
        var entity = await _clientRepository.GetAsync(expression);
        if (entity == null)
            return null;

        var client = ClientFactory.CreateModel(entity);
        return client;
    }
}
