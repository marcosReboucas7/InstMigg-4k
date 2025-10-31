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
            // Primeiro, verifique se já existe uma entrada rastreada com esse Id
            var trackedEntry = _context.ChangeTracker
                                       .Entries<Client>()
                                       .FirstOrDefault(e => e.Entity.Id == client.Id);

            if (trackedEntry != null)
            {
                // Se já existe uma entidade rastreada, atualize seus valores
                trackedEntry.CurrentValues.SetValues(client);
                await _context.SaveChangesAsync();
                return (Client)trackedEntry.Entity;
            }

            // Caso não haja uma instância rastreada, carregue a existente do banco
            var existing = await _context.Clients.FindAsync(client.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Cliente com ID {client.Id} não encontrado para atualização.");
            }

            // Copia os valores do DTO para a entidade rastreada
            _context.Entry(existing).CurrentValues.SetValues(client);
            await _context.SaveChangesAsync();
            return existing;
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
