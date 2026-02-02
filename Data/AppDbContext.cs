using Microsoft.EntityFrameworkCore;
using MaterialesEnSeco.Models;
using System;
using System.Configuration;

namespace MaterialesEnSeco.Data
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
                    .ConnectionStrings["UniplacDB"]
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
