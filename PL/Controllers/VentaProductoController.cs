using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            ML.Result result = BL.Producto.GetAll(producto);

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

        [HttpPost]
        public ActionResult Carrito()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CarritoPost(ML.Producto producto)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentaProductos = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                ventaProducto.Cantidad = ventaProducto.Cantidad = 1;
                ventaProducto.VentaProductos.Add(producto);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                var ventaSesion = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                foreach (var obj in ventaSesion)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());

                    ventaProducto.VentaProductos.Add(objProducto);
                }

                foreach (ML.Producto venta in ventaProducto.VentaProductos.ToList())
                {
                    if (producto.IdProducto == venta.IdProducto)
                    {
                        ventaProducto.Cantidad = ventaProducto.Cantidad + 1;
                    }
                    else
                    {
                        ventaProducto.Cantidad = ventaProducto.Cantidad = 1;
                        ventaProducto.VentaProductos.Add(producto);
                    }
                }
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
            }
            if (HttpContext.Session.GetString("Producto") != null)
            {
                ViewBag.MensajeVentaProducto = "Se agregó el producto a tu carrito";
            }
            else
            {
                ViewBag.MensajeVentaProducto = "Error al agregar el producto";
            }
            return View("Modal");
        }

        [HttpGet]

        public ActionResult ResumenCompra (ML.VentaProducto ventaProducto)
        {
            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();
            }
            else
            {
                var ventaSesion = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                ventaProducto.VentaProductos = new List<object>();

                foreach (var obj in ventaSesion)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    ventaProducto.VentaProductos.Add(objProducto);
                }
            }
            return View(ventaProducto);
        }
    }
}
