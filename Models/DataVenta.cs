using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialesEnSeco.Models
{
    internal class DataVenta
    {
        public int Id { get; set; }
        public int Venta_Id { get; set; }
        public string Categoria { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string FormaPago { get; set; }

        [ForeignKey("Venta_Id")]
        public Venta Venta { get; set; }
    }
}
