﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KomalliServer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KomalliEntities : DbContext
    {
        public KomalliEntities()
            : base("name=KomalliEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CashCut> CashCut { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Employee_FoodOrder> Employee_FoodOrder { get; set; }
        public virtual DbSet<FoodOrder> FoodOrder { get; set; }
        public virtual DbSet<FoodOrder_MenuCard> FoodOrder_MenuCard { get; set; }
        public virtual DbSet<FoodOrder_SetMenu> FoodOrder_SetMenu { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Logbook> Logbook { get; set; }
        public virtual DbSet<MenuCard> MenuCard { get; set; }
        public virtual DbSet<SetMenu> SetMenu { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
