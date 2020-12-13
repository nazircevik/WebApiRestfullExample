using Microsoft.EntityFrameworkCore;
using NazirCevik.WebApiDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NazirCevik.WebApiDemo.DataAcces
{
    public class NorthWindContext:DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true"
                );
        }
        public DbSet<Product> Products { get; set; }

    }
}
