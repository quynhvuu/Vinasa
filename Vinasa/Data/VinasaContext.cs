using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vinasa.Models;

namespace Vinasa.Data
{
    public class VinasaContext : DbContext
    {
        public VinasaContext (DbContextOptions<VinasaContext> options)
            : base(options)
        {
        }

        public DbSet<Vinasa.Models.Seminar> Seminar { get; set; }

        public DbSet<Vinasa.Models.SeminarParticipant> SeminarParticipant { get; set; }
    }
}
