    using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = HostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultApi = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Webapi"]);

                var responseTask = client.GetAsync("api/Usuario/GetAll");
                responseTask.Wait();
  
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    resultApi.Objects = new List<object>();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultApi.Objects.Add(resultUsuario);
                    }
                }
            }
            usuario.Usuarios = resultApi.Objects;
            //usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            //usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            //usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            //ML.Result result = BL.Usuario.GetAll(usuario);

            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //}
            //else
            //{
            //    result.Correct = false;
            //}
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;
            ML.Result result =  BL.Usuario.GetAll(usuario);

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
                    ML.Result resultApi = new ML.Result();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);

                        var responseTask = client.GetAsync("api/Usuario/GetById/?IdUsuario=" + IdUsuario);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            var resultItem = readTask.Result.Object;
                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            resultApi.Object = resultUsuario;
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error al mostrar los datos";
                            return View("modal");
                        }

                        usuario = (ML.Usuario)resultApi.Object; //Unboxing
                        usuario.Rol = new ML.Rol();
                        ML.Result resultRoles = BL.Rol.RolGetAll();
                        ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.IdColonia);
                        usuario.Rol.Roles = resultRoles.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    }
                }
            }
            return View(usuario);
            //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
            //        if (result.Correct)
            //        {
            //            usuario = (ML.Usuario)result.Object; //Unboxing
            //            usuario.Rol = new ML.Rol();
            //            ML.Result resultRoles = BL.Rol.RolGetAll();
            //            ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            //            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            //            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.IdColonia);
            //            usuario.Rol.Roles = resultRoles.Objects;
            //            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
            //            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            //            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            //            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
            //        }
            //        else
            //        {
            //            ViewBag.Mensaje = "Error al mostrar los datos";
            //            return View("modal");
            //        }
            //    }
            //}
            //return View(usuario);
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                IFormFile imagen = Request.Form.Files["fuImage"];
                if (imagen != null)
                {
                    byte[] ImagenByte = ConvertToBytes(imagen);
                    usuario.Imagen = Convert.ToBase64String(ImagenByte);
                }

                if (usuario.IdUsuario == 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);

                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/Usuario/Add", usuario);
                        postTask.Wait();

                        var resultService = postTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Registrado exitósamente";
                            return View("Modal");
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error al realizar el registro";
                            return View("Modal");
                        }
                    }

                    //ML.Result result = BL.Usuario.Add(usuario);

                    //if (result.Correct)
                    //{
                    //    ViewBag.Mensaje = "Registrado exitosamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "Error al realizar el registro";
                    //}
                    //return View("Modal");
                }
                else //Update
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);

                        var patchTask = client.PutAsJsonAsync<ML.Usuario>("api/Usuario/Update", usuario);
                        patchTask.Wait();

                        var resultService = patchTask.Result;

                        if(resultService.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Actualizado correctamente";
                            return View("Modal");
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error al actualizar el registro";
                            return View("Modal");
                        }
                    }

                    //ML.Result result = BL.Usuario.Update(usuario);

                    //if (result.Correct)
                    //{
                    //    ViewBag.Mensaje = "Registro actualizado correctamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "No se pudo actualzar el registro";
                    //}
                    //return View("modal");
                }
                
            }
            ML.Result resultRol = BL.Rol.RolGetAll();
            ML.Result resultPais = BL.Pais.GetAll();
            ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.IdColonia);
            usuario.Rol.Roles = resultRol.Objects;
            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
            return View(usuario);

        }

        public ActionResult UpdateStatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetById(IdUsuario);

            if (result.Correct)
            {
                usuario = (ML.Usuario)result.Object;

                usuario.Status = usuario.Status ? false : true;

                ML.Result resultUpdate = BL.Usuario.Update(usuario);

                if (resultUpdate.Correct)
                {
                    ViewBag.Mensaje = "Status actualizado";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar el status";
                }
            } 
            return View("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebApi"]);
                var deleteTask = client.DeleteAsync("api/Usuario/Delete/?IdUsuario=" + IdUsuario);
                deleteTask.Wait();

                var resultService = deleteTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Borrado exitósamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al borrar registro";
                }
            }
            
            //ML.Result result = BL.Usuario.Delete(usuario);

            //if (result.Correct)
            //{
            //    ViewBag.Mensaje = "Usuario borrado correctamente";
            //}
            //else
            //{
            //    ViewBag.Mensaje = "Error al eliminar el usuario";
            //}
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
