using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Direccions = new HashSet<Direccion>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Telefono { get; set; } = null!;
        public byte? IdRol { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = null!;
        public string? Celular { get; set; }
        public string? CURP { get; set; }
        public string? Imagen { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }

        //Alias
        public string NombreRol { get; set; }
        public string NombreColonia { get; set; }
        public string NombreMunicipio { get; set; }
        public string NombreEstado { get; set; }
        public string NombrePais { get; set; }

        //
        public int IdDireccion { get; set; }
        public string Calle { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }
        public int IdColonia { get; set; }
        public string CodigoPostal { get; set; }
        public int IdMunicipio { set; get; }
        public int IdEstado { set; get; }
        public int IdPais { set; get; }

    }
}
