using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    public class Usuario : ControllerBase
    {
        [HttpPost]
        [Route("api/Usuario/Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
           var result = BL.Usuario.Add(usuario);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
        }

        [HttpPut]
        [Route("api/Usuario/Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route ("api/Usuario/Delete")]
        public IActionResult Delete(int IdUsuario)
        {
            var result = BL.Usuario.Delete(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public IActionResult GetAll()
        {
            {
                ML.Usuario usuario = new ML.Usuario();
                var result = BL.Usuario.GetAll(usuario);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("api/Usuario/GetById")]

        public IActionResult GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            var result = BL.Usuario.GetById(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}