using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Entities;
using System.Security.Principal;

namespace ProjetoAPI.Context
{
    
    public class AgendaContext : DbContext
    {
        //Por ele a gente acessa ao banco de dados 
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
            
        }
       
        //Por ela a gente acessa os registro do banco 
        public DbSet<Contato> Contatos { get; set; }
    }
}
