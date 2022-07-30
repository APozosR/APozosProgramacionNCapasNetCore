﻿using System;
using System.Collections.Generic;

namespace PL
{
    public partial class Direccion
    {
        public int IdDireccion { get; set; }
        public string Calle { get; set; } = null!;
        public string? NumeroInterior { get; set; }
        public string NumeroExterior { get; set; } = null!;
        public int IdColonia { get; set; }
        public int IdUsuario { get; set; }

        public virtual Colonium IdColoniaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}