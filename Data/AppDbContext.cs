using Microsoft.EntityFrameworkCore;
using Pinturería.Models;
using System;
using System.Configuration;

namespace Pinturería.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DataVenta> DataVentas { get; set; }
        public DbSet<FormaPago> FormasPago { get; set; }
        public DbSet<CierreCaja> CierresCaja { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = ConfigurationManager
                    .ConnectionStrings["PintureriaDB"]
                    .ConnectionString;

                optionsBuilder.UseMySql(
                    connStr,
                    ServerVersion.AutoDetect(connStr),
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null
                        );
                    }
                );
            }
        }
    }
}
