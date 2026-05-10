using Microsoft.AspNetCore.Mvc;
using ProjetoEndereco.Data;
using ProjetoEndereco.Models;

namespace ProjetoEndereco.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(string usuario, string senha)
        {
            var usuarioBanco = _context.Usuarios
                .FirstOrDefault(u =>
                    u.UsuarioLogin == usuario &&
                    u.Senha == senha);

            if (usuarioBanco != null)
            {
                HttpContext.Session.SetInt32(
                    "UsuarioId",
                    usuarioBanco.Id);

                return RedirectToAction(
                    "Index",
                    "Endereco");
            }

            ViewBag.Erro = "Usuário ou senha inválidos";

            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
