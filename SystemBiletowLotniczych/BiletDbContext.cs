using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBiletowLotniczych
{
    /// <summary>
    /// Kontekst bazy danych Entity Framework odpowiedzialny za obsługę biletów lotniczych.
    /// </summary>
    public class BiletDbContext : DbContext
    {
        /// <summary>
        /// Reprezentuje tabelę biletów w bazie danych.
        /// </summary>
        public DbSet<Bilet> Bilets { get; set; }

    }
}
