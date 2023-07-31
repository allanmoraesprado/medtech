using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class DataContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Fichas> Fichas { get; set; }
        public DbSet<Logs> LogServer { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region OnModelCreating
        //OnModelCreating for custom entities if needed
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //base.OnModelCreating(builder);
        //...
        //}
        #endregion
    }
}
