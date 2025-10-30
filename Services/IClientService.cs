using InstMiggD.Entities;

namespace InstMiggD.Services
{
    public interface IClientService
    {

        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<Client>> GetClientsByUserAsync(string userName);
        Task<IEnumerable<Client>> GetClientsByMonthAsync(int month, int year);
        Task<IEnumerable<Client>> GetClientsByUserAndMonthAsync(string userName, int month, int year);

        Task<Client> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientAsync(Client client);
        Task<bool> DeleteClientAsync(int id);
    }
}
