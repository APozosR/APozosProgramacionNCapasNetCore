using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();
            ML.Result result = BL.Departamento.GetAllDepartamento();

            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
            }
            else
            {
                result.Correct = false;
            }
            return View(departamento);
        }

        [HttpGet]

        public ActionResult Form(int? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            if(IdDepartamento == null)
            {
                return View(departamento);
            }
            else
            {
                ML.Result result = BL.Departamento.GetByIdDepartamento(IdDepartamento.Value);
                if (result.Correct)
                {
                    departamento = (ML.Departamento)result.Object;
                   
                }
                else
                {
                    ViewBag.MensajeDeprtamento = "Error al mostrar los datos";
                    return View("Modal");
                }
            }
            return View(departamento);
        }

        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            if (departamento.IdDepartamento == 0)
            {
                ML.Result result = BL.Departamento.AddDepartamento(departamento);
                if (result.Correct)
                {
                    ViewBag.MensajeDepartamento = "Departamento registrado exitósamente";
                }
                else
                {
                    ViewBag.MensajeDepartamento = "Error al registrar el departamento";
                }
                return View("Modal");
            }
            else
            {
                ML.Result result = BL.Departamento.UpdateDepartamento(departamento);
                if (result.Correct)
                {
                    ViewBag.MensajeDepartamento = ("Departamento actualizado correctamente");
                }
                else
                {
                    ViewBag.MensajeDepartamento = ("Error al acualizar departamento");
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete (ML.Departamento departamento)
        {
            departamento.Area = new ML.Area();
            ML.Result result = BL.Departamento.DeleteDepartamento(departamento);
            if (result.Correct)
            {
                ViewBag.MensajeDepartamento = ("Departamento eliminado");
            }
            else
            {
                ViewBag.MensajeDepartamento = ("Error al eliminar el departamento");
            }
            return View("Modal");
        }
    }
}
