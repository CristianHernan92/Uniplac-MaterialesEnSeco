using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialesEnSeco.Models
{
    internal class CierreCaja
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjetaDebito { get; set; }
        public decimal TotalTarjetaCredito { get; set; }
        public decimal TotalMercadoPago { get; set; }
        public decimal TotalCuentaCorriente { get; set; }
        public decimal Transferencia { get; set; }
        public decimal TotalCierreCaja { get; set; }
    }
}
