using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.ModeliPodataka.DTO;

namespace EvidencijaNezaposlenih.Repozitorijum.Context
{
    public class EvidencijaNezaposlenihDBContext : DbContext
    {
        public EvidencijaNezaposlenihDBContext(DbContextOptions<EvidencijaNezaposlenihDBContext> options) : base(options)
        { 

        }

        public DbSet<Nezaposleni> Nezaposleni { get; set; }
        public DbSet<Poslodavac> Poslodavci { get; set; }
        public DbSet<RadniOdnos> RadniOdnosi { get; set; }
        public DbSet<PoslodavacPrikaz> PoslodavacPrikaz { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoslodavacPrikaz>().HasNoKey().ToView("PoslodavacPrikaz");
            modelBuilder.Entity<RadniOdnos>().HasKey(x => new { x.ID, x.NezaposleniID });
        }
    }

}
