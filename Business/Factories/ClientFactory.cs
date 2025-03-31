using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ClientFactory
{
    public static Client CreateModel(ClientEntity entity)
    {
        return new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName
        };
    }

}
