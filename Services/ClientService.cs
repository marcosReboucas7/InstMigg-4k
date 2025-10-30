using InstMiggD.Context;
using InstMiggD.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstMiggD.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado");

            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsByUserAsync(string userName)
        {
            return await _context.Clients
                .Where(c => c.CreatedBy == userName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByMonthAsync(int month, int year)
        {
            return await _context.Clients
                .Where(c => c.CreatedAt.Month == month && c.CreatedAt.Year == year)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByUserAndMonthAsync(string userName, int month, int year)
        {
            return await _context.Clients
                .Where(c => c.CreatedBy == userName &&
                            c.CreatedAt.Month == month &&
                            c.CreatedAt.Year == year)
                .ToListAsync();
        }


        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
