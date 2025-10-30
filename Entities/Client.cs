using System;
using System.ComponentModel.DataAnnotations;

namespace InstMiggD.Entities
{
    public enum ClientType
    {
        Instalacao,
        Migracao
    }

    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        // Vai definir se vai ser instalação ou migração
        public ClientType Type { get; set; } = ClientType.Instalacao;
        // Nome do vendedor
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // Propriedade caso seja instalação, que vai falar o valor do contrato
        public double Price { get; set; }
        // Propriedade caso o Type seja instalação
        public string? Contract { get; set; }
        // Propriedade caso o Type seja migração, que vai falar o novo valor do contrato
        public double NewPrice { get; set; }
        // Propriedade caso o Type seja migração, que vai falar o novo contrato
        public string? NewContract { get; set; }
    }
}
