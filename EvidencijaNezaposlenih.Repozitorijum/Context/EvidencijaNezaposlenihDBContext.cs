using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;

namespace EvidencijaNezaposlenih.Repozitorijum.Context
{
    public class EvidencijaNezaposlenihDBContext : DbContext
    {
        public DbSet<Nezaposleni> Nezaposleni { get; set; }
        public DbSet<Poslodavac> Poslodavci { get; set; }
        public DbSet<RadniOdnos> RadniOdnosi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EvidencijaNezaposlenih;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RadniOdnos>()
                .HasKey(ro => new { ro.PIB, ro.NezaposleniID });
        }
    }

}
