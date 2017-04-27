using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MustardSeedMission.Models;

namespace MustardSeedMission.Models
{
    public class MustardSeedMissionContext:DbContext
    {
        public MustardSeedMissionContext() : base("MustardSeedMissionContext") {}

        public virtual DbSet<BusinessCategory> BusinessCategory { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Complaints> Complaints { get; set; }
        public virtual DbSet<DonationBox> DonationBox { get; set; }
        public virtual DbSet<DonationHead> DonationHead { get; set; }
        public virtual DbSet<DonationList> DonationList { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Modification> Modification { get; set; }
        public virtual DbSet<PlanList> PlanList { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<SysFunctions> SysFunctions { get; set; }
        public virtual DbSet<SysPermission> SysPermission { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<BusinessCategory>()
              .HasMany(e => e.Stores)
              .WithOptional(e => e.BusinessCategory)
              .HasForeignKey(e => e.BusinessCode);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Complaints)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Categolgy);

            modelBuilder.Entity<DonationBox>()
                .HasMany(e => e.Stores)
                .WithOptional(e => e.DonationBox)
                .HasForeignKey(e => e.DBC);

            modelBuilder.Entity<DonationHead>()
                .HasMany(e => e.DonationLists)
                .WithRequired(e => e.DonationHead)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SysFunctions>()
                .HasMany(e => e.Groups)
                .WithRequired(e => e.SysFunction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.SysPermissions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}