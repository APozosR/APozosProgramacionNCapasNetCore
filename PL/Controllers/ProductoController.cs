using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                result.Correct = false;
            }
            return View(producto);
        }

        [HttpGet]

        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();

            ML.Result resultProveedor = BL.Proveedor.GetAllProveedor();
            ML.Result resultDepartamento = BL.Departamento.GetAllDepartamento();

            if (resultProveedor.Correct && resultDepartamento.Correct)
            {

                if (IdProducto == null)
                {
                    producto.Proveedor = new ML.Proveedor();
                    producto.Proveedor.Proveedores = resultProveedor.Objects;

                    producto.Departamento = new ML.Departamento();
                    producto.Departamento.Departamentos = resultDepartamento.Objects;
                    return View(producto);
                }
                else
                {
                    ML.Result result = BL.Producto.GetById(IdProducto.Value);
                    if (result.Correct)
                    {
                        producto = (ML.Producto)result.Object;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.Proveedores = resultProveedor.Objects;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.Departamentos = resultDepartamento.Objects;
                        return View(producto);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al mostrar los datos";
                    }
                }
            }
            return View(producto);
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            IFormFile imagen = Request.Form.Files["fuImage"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                producto.Imagen = Convert.ToBase64String(ImagenByte);
            }

            if (producto.IdProducto == 0)
            {
                ML.Result result = BL.Producto.Add(producto);

                if (result.Correct)
                {
                    ViewBag.MensajeProducto = "Registro exitoso";
                }
                else
                {
                    ViewBag.MensajeProducto = "Error al realizar el registro";
                }
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Producto.Update(producto);
                if (result.Correct)
                {
                    ViewBag.MensajeProducto = "Registro actalizado correctamente";
                }
                else
                {
                    ViewBag.MensajeProducto = "Error al actualizar el registro";
                }
                return View("Modal");
            }
        }
        [HttpGet]
        public ActionResult Delete(ML.Producto producto)
        {
            ML.Result result = BL.Producto.Delete(producto);
            if (result.Correct)
            {
                ViewBag.MensajeProducto = "Producto borrado con éxito";
            }
            else
            {
                ViewBag.MnesajeProducto = "Error al borrar el producto";
            }
            return View("Modal");
        }
        public static byte[] ConvertToBytes (IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}