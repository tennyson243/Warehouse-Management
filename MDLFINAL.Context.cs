﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gestion_Entrepot
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MDLString : DbContext
    {
        public MDLString()
            : base("name=MDLString")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ListColis> ListColis { get; set; }
        public virtual DbSet<View_Colis> View_Colis { get; set; }
        public virtual DbSet<View_Effet> View_Effet { get; set; }
        public virtual DbSet<View_FicheMagasin> View_FicheMagasin { get; set; }
        public virtual DbSet<View_Mouvement> View_Mouvement { get; set; }
        public virtual DbSet<View_Retrait> View_Retrait { get; set; }
        public virtual DbSet<View_Stock_Restant> View_Stock_Restant { get; set; }
    }
}
