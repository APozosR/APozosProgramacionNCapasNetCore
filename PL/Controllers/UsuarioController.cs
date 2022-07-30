using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                result.Correct = false;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            usuario.Rol = new ML.Rol();
            ML.Result resultRol = BL.Rol.RolGetAll();
            usuario.Pais = new ML.Pais();
            ML.Result resultPais = BL.Pais.GetAll();

            if (resultRol.Correct && resultPais.Correct)
            {
                if (IdUsuario == null)
                {
                    usuario.Rol = new ML.Rol();
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    return View(usuario);
                }
                else //UPDATE
                {
                    ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                    if (result.Correct)
                    {
                        usuario = (ML.Usuario)result.Object; //Unboxing
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.Roles = resultRol.Objects;

                        ML.Result resultEstado = BL.Estado.GetByIdPais(IdUsuario.Value);
                        ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(IdUsuario.Value);
                        ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(IdUsuario.Value);
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al mostrar los datos";
                        return View("modal");
                    }
                }
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile imagen = Request.Form.Files["fuImage"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                usuario.Imagen = Convert.ToBase64String(ImagenByte);
            }

            if (usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Usuario.Add(usuario);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registrado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al realizar el registro";
                }
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Usuario.Update(usuario);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo actualzar el registro";
                }
                return View("modal");
            }

        }

        [HttpGet]
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Usuario borrado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el usuario";
            }
            return View("modal");
        }

        public static byte[] ConvertToBytes (IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            
            return bytes;
        }

        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);
            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);
        }
    }
}
