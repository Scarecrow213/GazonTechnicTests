using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace lab2.Models
{
    public class TechnicContext : DbContext
    {
        public DbSet<Technic> Technics { get; set; }
        public DbSet<Storage> Storages { get; set; }
    }
}