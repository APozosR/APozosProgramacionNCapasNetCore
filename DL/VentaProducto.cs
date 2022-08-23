using System;
using System.Collections.Generic;

namespace DL
{
    public partial class VentaProducto
    {
        public int IdVentaProducto { get; set; }
        public int? IdVenta { get; set; }
        public int Cantidad { get; set; }
        public int? IdProducto { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Ventum? IdVentaNavigation { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Imagen { get; set; }
    }
}
