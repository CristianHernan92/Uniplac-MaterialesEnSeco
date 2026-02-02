using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialesEnSeco.Models
{
    internal class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Category_ID { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [ForeignKey("Category_ID")]
        public Categoria Categoria { get; set; }
    }
}
