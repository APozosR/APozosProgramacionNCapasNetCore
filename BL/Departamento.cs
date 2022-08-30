using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Departamento
    {
        public static ML.Result AddDepartamento(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoAdd '{departamento.Nombre}', {departamento.Area.IdArea}");

                    if(query > 0)
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

        public static ML.Result UpdateDepartamento(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoUpdate {departamento.IdDepartamento}, '{departamento.Nombre}',{departamento.Area.IdArea}");

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

        public static ML.Result DeleteDepartamento(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoDelete {departamento.IdDepartamento}");

                    if  (query > 0 )
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
        public static ML.Result GetAllDepartamento()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;

                            result.Objects.Add(departamento);
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

        public static ML.Result GetByIdDepartamento(int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetById {IdDepartamento}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = query.IdDepartamento;
                        departamento.Nombre = query.Nombre;

                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = query.IdArea.Value;
                        result.Object = departamento;
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
        public static ML.Result GetByIdArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.APozosProgramacionNCapasContext context = new DL.APozosProgramacionNCapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdArea}").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;

                            result.Objects.Add(departamento);
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
