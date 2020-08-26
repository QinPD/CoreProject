using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models.Entity;

namespace WebCoreProject.Models
{
    public partial class WebCoreProjectContext : DbContext
    {
        public static string ConStr { get; set; }

        public WebCoreProjectContext(DbContextOptions<WebCoreProjectContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<Shop> Shop { get; set; }
        //public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly serviceAss = Assembly.Load("Models");
            serviceAss.GetTypes().Where(t => t.IsDefined(typeof(TableAttribute))).ToList().ForEach(etype => modelBuilder.Entity(etype));
        }
    }
}
