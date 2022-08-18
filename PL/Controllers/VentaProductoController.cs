using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ML.Result result = BL.VentaProducto.GetAll();

            if (result.Correct)
            {
                ventaProducto.VentaProductos = result.Objects;
            }
            else
            {
                result.Correct = false;
            }

            return View(ventaProducto);
        }
    }
}
