using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}','{producto.PrecioUnitario}','{producto.Stock}','{producto.Descripcion}','{producto.Imagen}',{producto.Proveedor.IdProveedor},{producto.Departamento.IdDepartamento}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update (ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto},'{producto.Nombre}','{producto.PrecioUnitario}','{producto.Stock}','{producto.Descripcion}','{producto.Imagen}','{producto.Proveedor.IdProveedor}','{producto.Departamento.IdDepartamento}'");

                    if (query > 0)
                    {
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

        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoDelete '{producto.IdProducto}'");
                   
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll(ML.Producto productoBusqueda)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll '{productoBusqueda.Departamento.IdDepartamento}'").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = obj.PrecioUnitario;
                            producto.Stock = obj.Stock;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;

                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.Nombre = obj.NombreProveedor;

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.Nombre = obj.NombreDepartamento;


                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var obj = context.Productos.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Producto producto    = new ML.Producto();
                        producto.IdProducto = obj.IdProducto;
                        producto.Nombre = obj.Nombre;
                        producto.PrecioUnitario = obj.PrecioUnitario;
                        producto.Stock = obj.Stock;
                        producto.Descripcion = obj.Descripcion;
                        producto.Imagen = obj.Imagen;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = obj.IdProveedor.Value;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;

                        result.Object = producto;
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

        public static ML.Result ConvertirExcelDataTable(string connectionString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Hoja 1$]";

                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;

                        OleDbDataAdapter da = new OleDbDataAdapter();

                        da.SelectCommand = cmd;

                        DataTable tableProducto = new DataTable();

                        da.Fill(tableProducto);

                        if (tableProducto.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableProducto.Rows)
                            {
                                ML.Producto producto = new ML.Producto();
                                producto.Nombre = row[0].ToString();
                                producto.PrecioUnitario = decimal.Parse(row[1].ToString());
                                producto.Stock = int.Parse(row[2].ToString());
                                producto.Descripcion = row[3].ToString();

                                producto.Proveedor = new ML.Proveedor();
                                producto.Proveedor.IdProveedor = int.Parse(row[4].ToString());

                                producto.Departamento = new ML.Departamento();
                                producto.Departamento.IdDepartamento = int.Parse(row[5].ToString());

                                result.Objects.Add(producto);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableProducto;

                        if (tableProducto.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;

                            result.ErrorMessage = "No existen registros en Excel";
                        }
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

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;

                foreach (ML.Producto producto in Objects)
                {
                    ML.ExcelErrores error = new ML.ExcelErrores();
                    error.IdRegistro = i;

                    if (producto.Nombre == "")
                    {
                        error.Message += "Ingrese en nombre ";
                    }
                    if (producto.PrecioUnitario.ToString() == "")
                    {
                        error.Message += "Ingrese precio unitario";
                    }
                    if (producto.Stock.ToString() == "")
                    {
                        error.Message += "Ingrese Stock";
                    }
                    if (producto.Descripcion == "")
                    {
                        error.Message += "Ingrese Descripción";
                    }
                    if (producto.Proveedor.IdProveedor.ToString() == "")
                    {
                        error.Message += "Ingrese IdProveedor";
                    }
                    if (producto.Departamento.IdDepartamento.ToString() == "")
                    {
                        error.Message += "Ingrese IdDrepartamento";
                    }
                   
                    if (error.Message != null)
                    {
                        result.Objects.Add(error);
                    }

                    i++;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
