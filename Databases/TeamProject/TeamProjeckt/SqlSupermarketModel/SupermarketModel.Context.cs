﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlSupermarketModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class supermarketEntities : DbContext
    {
        public supermarketEntities()
            : base("name=supermarketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Mesure> Mesures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SellsReport> SellsReports { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}