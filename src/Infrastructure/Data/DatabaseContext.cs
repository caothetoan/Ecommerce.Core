using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vnit.ApplicationCore.Entities;

namespace Vnit.Infrastructure.Data
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DatabaseManager.SetDbInitializer<DatabaseContext>(null);//new CreateDatabaseIfNotExists<FirstDbContext>()

            var typesToRegister = typeof(DatabaseContext).Assembly.GetTypes()
          .Where(type => !string.IsNullOrEmpty(type.Namespace))
          .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
              type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
           
            base.OnModelCreating(modelBuilder);
        }

     
        public new DbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        
    }
}
