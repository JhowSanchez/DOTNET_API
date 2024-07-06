using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Context;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        //Listar todos contatos no banco
        public IActionResult Index()
        {
            var contatos = _context.Contatos.ToList();
            return View(contatos);
        }

        //Criar novo Contato ## Esse metodo e um GET e quando eu entra em CRIAR vai abrir a pagina e o Criar abaixo e um POST que quando clicando no botao
        //Criar da pagina Criar ele entrara no metodo abaixo que e um POST
        public IActionResult Criar() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Contato contato)
        {
             if(ModelState.IsValid)
             { 
                _context.Contatos.Add(contato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             }
            return View(contato);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if(contato == null) 
                return RedirectToAction(nameof(Index));
            return View(contato);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Contato contato)
        {
            var contatoBanco = await _context.Contatos.FindAsync(contato.Id);

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);

            if (contato == null)
                return RedirectToAction(nameof(Index));
            return View(contato);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if(contato == null)
                return RedirectToAction(nameof(Index));
            return View(contato);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Contato contato)
        {
            var contatoBanco = await _context.Contatos.FindAsync(contato.Id);
            _context.Contatos.Remove(contatoBanco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
