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
                    var query = context.Productos.FromSqlRaw($"VentaProductoGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.VentaProducto ventaProducto= new ML.VentaProducto();
                            ventaProducto.NombreProducto = obj.Nombre;
                            ventaProducto.Descripcion = obj.Descripcion;
                            ventaProducto.PrecioUnitario = obj.PrecioUnitario;
                            ventaProducto.Imagen = obj.Imagen; 

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
