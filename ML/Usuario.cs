using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Ingrese Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese Apellido Paterno")]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string? ApellidoMaterno { get; set; }

        public string Telefono { get; set; }
        public ML.Rol? Rol { get; set; }
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string? Celular { get; set; }


        [StringLength(18, ErrorMessage = "La longitud del CURP no es válida.")]
        public string CURP { get; set; }
        public string Imagen { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Direccion? Direccion { get; set; }
        public ML.Pais Pais { get; set; }
        public bool Status { get; set; }


        //Alias
        public string? NombreRol { get; set; }
        public string? NombreColonia { get; set; }
        public string? NombreMunicipio { get; set; }
        public string? NombreEstado { get; set; }
        public string? NombrePais { get; set; }

        //Propiedades del SP
        public int? IdDireccion { get; set; }
        public string? Calle { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }
        public string? CodigoPostal { get; set; }
        public int? IdColonia { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdEstado { get; set; }
        public int? IdPais { get; set; }
    }
}
