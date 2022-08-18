using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProveedorController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Proveedor proveedor = new ML.Proveedor();
            ML.Result result = BL.Proveedor.GetAllProveedor();

            if (result.Correct)
            {
                proveedor.Proveedores = result.Objects;
            }
            else
            {
                result.Correct = false;
            }
            return View(proveedor);
        }

        [HttpGet]
        public ActionResult Form(int? IdProveedor)
        {
            ML.Proveedor proveedor = new ML.Proveedor();

            if (IdProveedor == null)
            {
                return View(proveedor);
            }
            else
            {
                ML.Result result = BL.Proveedor.GetByIdProveedor(IdProveedor.Value);
                if (result.Correct)
                {
                    proveedor = (ML.Proveedor)result.Object;
                    return View(proveedor);
                }
                else
                {
                    ViewBag.MensajeProveedor = "Error al mostrar los datos";
                }
                return View("Modal");
            }
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult Form(ML.Proveedor proveedor)
        {
            if (proveedor.IdProveedor == 0)
            {
                ML.Result result = BL.Proveedor.AddProveedor(proveedor);

                if (result.Correct)
                {
                    ViewBag.MensajeProveedor = "Proveedor registrado exitósamente";
                }
                else
                {
                    ViewBag.MensajeProveedor = "Error al registrar";
                }
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Proveedor.UpdateProveedor(proveedor);
                if (result.Correct)
                {
                    ViewBag.MensajeProveedor = "Proveedor actualizado correctamente";
                }
                else
                {
                    ViewBag.MensajeProveedor = "Error al actualizar";
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Proveedor proveedor)
        {
            ML.Result result = BL.Proveedor.DeleteProveedor(proveedor);
            if (result.Correct)
            {
                ViewBag.MensajeProveedor = "Proveedor borrado con éxito";
            }
            else
            {
                ViewBag.MnesajeProveedor = "Error al borrar el proveedor";
            }
            return View("Modal");
        }
    }
}
