using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaProducto
    {
        public int IdVentaProducto {get; set;}
        public ML.Venta Venta { get; set; }
        public int Cantidad { get; set; }
        public ML.Producto Producto { get; set; }
        public List<object> VentaProductos { get; set; }

        //SP
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Imagen { get; set; }

    }
}
