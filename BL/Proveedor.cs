using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result AddProveedor(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorAdd '{proveedor.Nombre}','{proveedor.Telefono}'");

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

        public static ML.Result UpdateProveedor(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorUpdate {proveedor.IdProveedor},'{proveedor.Nombre}','{proveedor.Telefono}'");
                    
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

        public static ML.Result DeleteProveedor(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorDelete {proveedor.IdProveedor}");

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
        public static ML.Result GetAllProveedor()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Proveedors.FromSqlRaw($"ProveedorGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = obj.IdProveedor;
                            proveedor.Telefono = obj.Telefono;
                            proveedor.Nombre = obj.Nombre;

                            result.Objects.Add(proveedor);
                        }
                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetByIdProveedor(int IdProveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Proveedors.FromSqlRaw($"ProveedorGetById {IdProveedor}").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Proveedor proveedor = new ML.Proveedor();

                        proveedor.IdProveedor = query.IdProveedor;
                        proveedor.Nombre = query.Nombre;
                        proveedor.Telefono = query.Telefono;

                        result.Object = proveedor;
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
    }
}
