using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;
using ProjetoAPI.Entities;

namespace ProjetoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contato contato)
        {
            _context.Add(contato);
           await _context.SaveChangesAsync();

            //return Ok(contato);
            //Retorna uma Response Headers onde mostra o endereço de onde o novo contato foi criado e onde iremos ver ele
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);

            if(contato == null)
                return NotFound();
            return Ok(contato);
        }

        [HttpGet("ObterPorNome")]
        public async Task <IActionResult> ObterTodos(string nome)
        {
            var contatos = await _context.Contatos.Where(x => x.Nome.Contains(nome)).ToListAsync();
            return Ok(contatos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Contato contato)
        {
            var contatoBanco = await _context.Contatos.FindAsync(id);

            if (contatoBanco == null)
                return NotFound();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            await _context.SaveChangesAsync();

            return Ok(contatoBanco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);

            if(contato == null)
                return NotFound();

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
