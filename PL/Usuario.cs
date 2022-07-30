using System;
using System.Collections.Generic;

namespace PL
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
        public string? Curp { get; set; }
        public string? Imagen { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }
    }
}
