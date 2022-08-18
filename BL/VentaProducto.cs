using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class VentaProducto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.VentaProductos.FromSqlRaw($"VentaProductoGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.VentaProducto ventaProducto= new ML.VentaProducto();

                            ventaProducto.IdVentaProducto = obj.IdVentaProducto;

                            ventaProducto.Venta = new ML.Venta();
                            ventaProducto.Venta.IdVenta = obj.IdVenta.Value;
                            ventaProducto.Cantidad = obj.Cantidad;

                            ventaProducto.Producto = new ML.Producto();
                            ventaProducto.Producto.IdProducto = obj.IdProducto.Value;
                            //ventaProducto.Producto.Nombre = obj.Nombre;
                            //ventaProducto.Producto.Descripcion = obj.Descripcion;
                            //ventaProducto.Producto.Imagen = obj.Imagen; 

                            result.Objects.Add(ventaProducto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
