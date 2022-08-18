using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Producto
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            StreamReader archivo = new StreamReader(@"C:\Users\digis\Documents\LayoutProducto.txt");

            string line;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            line = archivo.ReadLine();

            while ((line = archivo.ReadLine()) != null)
            {
                string[] datos = line.Split('|');

                ML.Producto producto = new ML.Producto();

                producto.Nombre = datos[0];
                producto.PrecioUnitario = decimal.Parse(datos[1]);
                producto.Stock = Convert.ToInt32(datos[2]);
                producto.Descripcion = datos[3];

                producto.Proveedor = new ML.Proveedor();
                producto.Proveedor.IdProveedor = Convert.ToInt32(datos[4]);

                producto.Departamento = new ML.Departamento();
                producto.Departamento.IdDepartamento = Convert.ToInt32(datos[5]);

                result = BL.Producto.Add(producto);

                if (result.Correct == false)
                {
                    resultErrores.Objects.Add(
                        "No se insertó el nombre: " + producto.Nombre + " " +
                        "No se insertó el precio unitario: " + producto.PrecioUnitario + " " +
                        "No se insertó el stock: " + producto.Stock + " " +
                        "No se insertó la descripción: " + producto.Descripcion + " " +
                        "No se insertó el IdProveedor: " + producto.Proveedor.IdProveedor + " " +
                        "No se insertó el IdDepartamento: " + producto.Departamento.IdDepartamento + " " +
                        result.ErrorMessage
                        );
                }
            }
            archivo.Close();

            if (resultErrores.Objects != null)
            {
                TextWriter tw = new StreamWriter(@"C:\Users\digis\Documents\ErroresCargaMasiva.txt");

                foreach (string error in resultErrores.Objects)
                {
                    tw.WriteLine(error);
                    Console.WriteLine(error);
                }
                tw.Close();
            }
            return result;
        }
    }
}
