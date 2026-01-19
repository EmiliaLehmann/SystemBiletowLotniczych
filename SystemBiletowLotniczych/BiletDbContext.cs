using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBiletowLotniczych
{
    public class BiletDbContext:DbContext
    {
        public DbSet<Bilet> Bilets { get; set; }

    }
}
