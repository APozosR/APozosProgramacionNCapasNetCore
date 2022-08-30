using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Login(ML.Usuario usuarioLogin)
        {
            ML.Result result = BL.Usuario.GetByEmail(usuarioLogin.Email);
            if (result.Correct)           
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuarioLogin.Password == usuario.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginMessage = "Datos incorrectos";
                    return View("Modal");
                }
            }
            else
            {
                ViewBag.LoginMessage = "El correo no está resgistrado";
            }
            return View("Modal");

        }
    }
}
