using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public ML.Rol? Rol { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string? Celular { get; set; }
        public string CURP { get; set; }
        public string Imagen { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Direccion Direccion { get; set; }
        public ML.Pais Pais { get; set; }

        //Alias
        public string NombreRol { get; set; }
        public string NombreColonia { get; set; }
        public string NombreMunicipio { get; set; }
        public string NombreEstado { get; set; }
        public string NombrePais { get; set; }

        //Propiedades del SP
        public int IdDireccion { get; set; }
        public string Calle { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }
        public string CodigoPostal { get; set; }
        public int IdColonia { get; set; }
        public int IdMunicipio { get; set; }
        public int IdEstado { get; set; }
        public int IdPais { get; set; }
    }
}
