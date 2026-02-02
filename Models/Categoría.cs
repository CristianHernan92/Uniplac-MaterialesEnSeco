using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialesEnSeco.Models
{
    internal class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación uno a muchos con Productos
        public ICollection<Producto> Productos { get; set; }
    }
}
