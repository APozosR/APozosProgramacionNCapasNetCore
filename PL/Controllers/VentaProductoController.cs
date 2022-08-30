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
            producto.Departamento.Area = new ML.Area();

            ML.Result resultArea = BL.Area.GetAllArea();
            producto.Departamento.Area.Areas = resultArea.Objects;
            producto.Departamento.IdDepartamento = (producto.Departamento.IdDepartamento == 0) ? 0 : producto.Departamento.IdDepartamento;

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
        public ActionResult GetAll(ML.Producto producto)
        {
            producto.Departamento.Area = new ML.Area();
            ML.Result resultArea = BL.Area.GetAllArea();
            producto.Departamento.Area.Areas = resultArea.Objects;
            producto.Departamento.IdDepartamento = (producto.Departamento.IdDepartamento == 0) ? 0 : producto.Departamento.IdDepartamento;

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
        public ActionResult CarritoPost(ML.Producto producto)  //METODO QUE VA LLAMAR EL BOTON DE AGREGAR
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentaProductos = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                producto.Stock = producto.Stock = 1;
                ventaProducto.VentaProductos.Add(producto);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentaProductos));
                
            }
            else
            {
                var ventaSesion = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                foreach (var obj in ventaSesion)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());

                    ventaProducto.VentaProductos.Add(objProducto);
                }

                foreach (ML.Producto venta in ventaProducto.VentaProductos.ToList()) //VALIDAR SI no esta en la lista
                {
                    if (producto.IdProducto == venta.IdProducto)
                    {
                        venta.Stock = venta.Stock + 1;
                    }

                    else
                    {
                        producto.Stock = producto.Stock = 1;
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

        public ActionResult ResumenCompra (ML.VentaProducto ventaProducto) //VER CARRITo TABLE
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
        public JsonResult DepartamentoGetByIdArea(int IdArea)
        {
            ML.Result result = BL.Departamento.GetByIdArea(IdArea);
            return Json(result.Objects);
        }
    }
}
