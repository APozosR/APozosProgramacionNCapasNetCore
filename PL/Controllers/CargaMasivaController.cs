using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = HostingEnvironment;
        }


        [HttpGet]
        public IActionResult CargaMasivaProducto()
        {
            ML.Result result = new ML.Result();

            return View(result);
        }

        [HttpPost]
        public IActionResult CargaMasivaProducto(ML.Producto producto)
        {
            IFormFile archivo = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (archivo != null)
                {
                    if (archivo.Length > 0)
                    {
                        string FileName = Path.GetFileName(archivo.FileName);
                        string folderPath = _configuration["PathFolder:value"];
                        string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                        string extensionModulo = _configuration["TipoExcel"];

                        if (extensionArchivo == extensionModulo)
                        {
                            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(FileName)) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlxs";
                            if (!System.IO.File.Exists(filePath))
                            {
                                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                                {
                                    archivo.CopyTo(stream);
                                }

                                string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                                ML.Result resultProductos = BL.Producto.ConvertirExcelDataTable(connectionString);

                                if (resultProductos.Correct)
                                {
                                    ML.Result resultValidation = BL.Producto.ValidarExcel(resultProductos.Objects);
                                    if(resultValidation.Objects.Count == 0) 
                                    {
                                        resultValidation.Correct = true;
                                        HttpContext.Session.SetString("PathArchivo", filePath);
                                    }
                                    return View(resultValidation);
                                }
                                else
                                {
                                    ViewBag.Message = "No se encontraron registros/ Tenía errores";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Seleccione un archivo válido (.xlsx)";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No tiene datos el archivo";
                    }
                }
                else
                {
                    ViewBag.Message = "No se seleccionó el achivo";
                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Producto.ConvertirExcelDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Producto productoItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Producto.Add(productoItem);
                        producto.Proveedor = new ML.Proveedor();
                        producto.Departamento = new ML.Departamento();
                        if (!resultAdd.Correct)
                        {

                            resultErrores.Objects.Add("No se insertó el nombre: " + producto.Nombre + 
                                "\nNo se insertó el precio unitario:" +producto.PrecioUnitario + 
                                "\nNo se insertó el stock:" + producto.Stock + 
                                "\nNo se insertó la descripción:" + producto.Descripcion + 
                                "\nNo se insertó el IdProveedor:" + producto.Proveedor.IdProveedor + 
                                "\nNo se insertó el IdDepartamento:" + producto.Departamento.IdDepartamento);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {
                        string folderError = _configuration["PathFolderError:value"];
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, folderError + @"\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Algunos productos no han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Mesage = "Los productos han sido registrados correctamente";
                    }
                }
            }
            return View("Modal");
        }
    }
}
