using Microsoft.AspNet.Identity.EntityFramework;
using oa.db.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oa.db.Context
{
    public class OAContext : DbContext, IOAContext
    {
        public OAContext() : base("DB")
        {

        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        public DbSet<User> User { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Personnel> Personnel { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

      
    }
}
