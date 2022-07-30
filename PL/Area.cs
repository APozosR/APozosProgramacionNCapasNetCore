using System;
using System.Collections.Generic;

namespace PL
{
    public partial class Area
    {
        public Area()
        {
            Departamentos = new HashSet<Departamento>();
        }

        public int IdArea { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
