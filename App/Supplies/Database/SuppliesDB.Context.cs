﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Supplies.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SuppliesDBEntities : DbContext
    {
        private static SuppliesDBEntities _context;

        public static SuppliesDBEntities GetContext()
        {
            if (_context == null)
                _context = new SuppliesDBEntities();
            return _context;
        }

        public SuppliesDBEntities()
            : base("name=SuppliesDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Components> Components { get; set; }
        public virtual DbSet<Components_type> Components_type { get; set; }
        public virtual DbSet<Ordered_components> Ordered_components { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SuppliesT> Supplies { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
