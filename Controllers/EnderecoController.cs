using System.Text;
using Microsoft.AspNetCore.Mvc;
using ProjetoEndereco.Data;
using ProjetoEndereco.Models;

namespace ProjetoEndereco.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarioId =
                HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction(
                    "Index",
                    "Login");
            }

            var enderecos = _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToList();

            return View(enderecos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var endereco = _context.Enderecos.Find(id);

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost]
        public IActionResult Edit(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var endereco = _context.Enderecos.Find(id);

            if (endereco == null)
            {
                return NotFound();
            }

            _context.Enderecos.Remove(endereco);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ExportarCsv()
        {
            var usuarioId =
                HttpContext.Session.GetInt32("UsuarioId");

            var enderecos = _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToList();

            var csv = new StringBuilder();

            csv.AppendLine(
                "CEP,Logradouro,Complemento,Bairro,Cidade,UF,Número");

            foreach (var endereco in enderecos)
            {
                csv.AppendLine(
                    $"{endereco.Cep}," +
                    $"{endereco.Logradouro}," +
                    $"{endereco.Complemento}," +
                    $"{endereco.Bairro}," +
                    $"{endereco.Cidade}," +
                    $"{endereco.Uf}," +
                    $"{endereco.Numero}");
            }

            return File(
                Encoding.UTF8.GetBytes(csv.ToString()),
                "text/csv",
                "enderecos.csv");
        }

        [HttpPost]
        public IActionResult Create(Endereco endereco)
        {
            var usuarioId =
                HttpContext.Session.GetInt32("UsuarioId");

            endereco.UsuarioId = usuarioId.Value;

            if (ModelState.IsValid)
            {
                _context.Enderecos.Add(endereco);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(endereco);
        }
    }
}
