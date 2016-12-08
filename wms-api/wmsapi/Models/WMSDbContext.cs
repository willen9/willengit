using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace wmsapi.Models
{
    //[DbConfigurationType(typeof(WMSDbConfiguration))]
    public class WMSDbContext : DbContext
    {
        public WMSDbContext(APIDatabaseConfiguration configuration)
        {
            this.Database.Connection.ConnectionString = configuration.ActiveConnectionString;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<CMMDG> CMMDG { get; set; }
        public DbSet<CMMDI> CMMDI { get; set; }
        public DbSet<CMMDP> CMMDP { get; set; }
        public DbSet<CMMDQ> CMMDQ { get; set; }
        public DbSet<CMMDR> CMMDR { get; set; }
        public DbSet<CMMDX> CMMDX { get; set; }
        public DbSet<MMDAA> MMDAA { get; set; }
        public DbSet<MMDAB> MMDAB { get; set; }
        public DbSet<MMDBA> MMDBA { get; set; }
        public DbSet<MMDBB> MMDBB { get; set; }
        public DbSet<MMDCA> MMDCA { get; set; }
        public DbSet<MMDCB> MMDCB { get; set; }
        public DbSet<MMDEA> MMDEA { get; set; }
        public DbSet<MMDEB> MMDEB { get; set; }
        public DbSet<MMDFA> MMDFA { get; set; }
        public DbSet<MMDFB> MMDFB { get; set; }
        public DbSet<MMDGA> MMDGA { get; set; }
        public DbSet<MMDGB> MMDGB { get; set; }
        public DbSet<MMDHA> MMDHA { get; set; }
        public DbSet<MMDHB> MMDHB { get; set; }
        public DbSet<MMDKA> MMDKA { get; set; }
        public DbSet<MMDKB> MMDKB { get; set; }
        public DbSet<MMDLA> MMDLA { get; set; }
        public DbSet<MMDLB> MMDLB { get; set; }
        public DbSet<MMDMA> MMDMA { get; set; }
        public DbSet<MMDMB> MMDMB { get; set; }
        public DbSet<MMDNA> MMDNA { get; set; }
        public DbSet<MMDNB> MMDNB { get; set; }
        public DbSet<MMMDB> MMMDB { get; set; }
        public DbSet<MMMDG> MMMDG { get; set; }
        public DbSet<MMSOB> MMSOB { get; set; }
        public DbSet<MMSOC> MMSOC { get; set; }
        public DbSet<MMSOD> MMSOD { get; set; }
        public DbSet<CMMDC> CMMDC { get; set; }
        public DbSet<CMMDF> CMMDF { get; set; }
        public DbSet<CMMDH> CMMDH { get; set; }
        public DbSet<SMSCA> SMSCA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<CMMDG>()
                .HasKey(c => new { c.MDG001 });

            modelBuilder.Entity<CMMDI>()
                .HasKey(c => new { c.MDI001 });

            modelBuilder.Entity<CMMDP>()
                .HasKey(c => new { c.MDP001 });

            modelBuilder.Entity<CMMDQ>()
                .HasKey(c => new { c.MDQ001, c.MDQ002 });

            modelBuilder.Entity<CMMDR>()
                .HasKey(c => new { c.MDR001, c.MDR002, c.MDR003 });

            modelBuilder.Entity<CMMDX>()
                .HasKey(c => new { c.MDX001 });

            modelBuilder.Entity<MMDAA>()
                .HasKey(c => new { c.DAA001, c.DAA002 });

            modelBuilder.Entity<MMDAB>()
                .HasKey(c => new { c.DAB001, c.DAB002, c.DAB003 });

            modelBuilder.Entity<MMDBA>()
                .HasKey(c => new { c.DBA001, c.DBA002 });

            modelBuilder.Entity<MMDBB>()
                .HasKey(c => new { c.DBB001, c.DBB002, c.DBB003 });

            modelBuilder.Entity<MMDCA>()
                .HasKey(c => new { c.DCA001, c.DCA002 });

            modelBuilder.Entity<MMDCB>()
                .HasKey(c => new { c.DCB001, c.DCB002, c.DCB003 });

            modelBuilder.Entity<MMDEA>()
                .HasKey(c => new { c.DEA001, c.DEA002 });

            modelBuilder.Entity<MMDEB>()
                .HasKey(c => new { c.DEB001, c.DEB002, c.DEB003 });

            modelBuilder.Entity<MMDFA>()
                .HasKey(c => new { c.DFA001, c.DFA002 });

            modelBuilder.Entity<MMDFB>()
                .HasKey(c => new { c.DFB001, c.DFB002, c.DFB003 });

            modelBuilder.Entity<MMDGA>()
                .HasKey(c => new { c.DGA001, c.DGA002 });

            modelBuilder.Entity<MMDGB>()
                .HasKey(c => new { c.DGB001, c.DGB002, c.DGB003 });

            modelBuilder.Entity<MMDHA>()
                .HasKey(c => new { c.DHA001, c.DHA002 });

            modelBuilder.Entity<MMDHB>()
                .HasKey(c => new { c.DHB001, c.DHB002, c.DHB003 });

            modelBuilder.Entity<MMDKA>()
                .HasKey(c => new { c.DKA001, c.DKA002 });

            modelBuilder.Entity<MMDKB>()
                .HasKey(c => new { c.DKB001, c.DKB002, c.DKB003 });

            modelBuilder.Entity<MMDLA>()
                .HasKey(c => new { c.DLA001, c.DLA002 });

            modelBuilder.Entity<MMDLB>()
                .HasKey(c => new { c.DLB001, c.DLB002, c.DLB003 });

            modelBuilder.Entity<MMDMA>()
                .HasKey(c => new { c.DMA001, c.DMA002 });

            modelBuilder.Entity<MMDMB>()
                .HasKey(c => new { c.DMB001, c.DMB002, c.DMB003 });

            modelBuilder.Entity<MMDNA>()
                .HasKey(c => new { c.DNA001, c.DNA002 });

            modelBuilder.Entity<MMDNB>()
                .HasKey(c => new { c.DNB001, c.DNB002, c.DNB003 });

            modelBuilder.Entity<MMMDB>()
                .HasKey(c => new { c.MDB001 });

            modelBuilder.Entity<MMMDG>()
                .HasKey(c => new { c.MDG001 });

            modelBuilder.Entity<MMSOB>()
                .HasKey(c => new { c.SOB001, c.SOB002, c.SOB003, c.SOB004 });

            modelBuilder.Entity<MMSOC>()
                .HasKey(c => new { c.SOC001, c.SOC002, c.SOC003, c.SOC004, c.SOC005 });

            modelBuilder.Entity<MMSOD>()
                .HasKey(c => new { c.SOD001, c.SOD002, c.SOD003, c.SOD004, c.SOD005 });

            modelBuilder.Entity<CMMDC>()
                .HasKey(c => new { c.MDC001 });

            modelBuilder.Entity<CMMDF>()
                .HasKey(c => new { c.MDF001 });

            modelBuilder.Entity<CMMDH>()
                .HasKey(c => new { c.MDH001 });

            modelBuilder.Entity<SMSCA>()
                .HasKey(c => new { c.SCA001});
        }
    }
}