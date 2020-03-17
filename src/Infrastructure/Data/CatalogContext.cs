using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.BasketAggregate;
using Vnit.ApplicationCore.Entities.OrderAggregate;

namespace Vnit.Infrastructure.Data
{

    public class CatalogContext : DbContext, IDatabaseContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Basket>(ConfigureBasket);
            //modelBuilder.Entity<CatalogBrand>(ConfigureCatalogBrand);
            //modelBuilder.Entity<CatalogType>(ConfigureCatalogType);
            //modelBuilder.Entity<CatalogItem>(ConfigureCatalogItem);
            //modelBuilder.Entity<Order>(ConfigureOrder);
            //modelBuilder.Entity<OrderItem>(ConfigureOrderItem);

            var typesToRegister = typeof(CatalogContext).Assembly.GetTypes()
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

        //private void ConfigureBasket(EntityTypeBuilder<Basket> builder)
        //{
        //    var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));

        //    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        //}

        //private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder)
        //{
        //    builder.ToTable("Catalog");

        //    builder.Property(ci => ci.Id)
        //        //.ForSqlServerUseSequenceHiLo("catalog_hilo")
        //        .IsRequired();

        //    builder.Property(ci => ci.Name)
        //        .IsRequired(true)
        //        .HasMaxLength(50);

        //    builder.Property(ci => ci.Price)
        //        .IsRequired(true);

        //    builder.Property(ci => ci.PictureUri)
        //        .IsRequired(false);

        //    builder.HasOne(ci => ci.CatalogBrand)
        //        .WithMany()
        //        .HasForeignKey(ci => ci.CatalogBrandId);

        //    builder.HasOne(ci => ci.CatalogType)
        //        .WithMany()
        //        .HasForeignKey(ci => ci.CatalogTypeId);
        //}

        //private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
        //{
        //    builder.ToTable("CatalogBrand");

        //    builder.HasKey(ci => ci.Id);

        //    builder.Property(ci => ci.Id)
        //       //.ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
        //       .IsRequired();

        //    builder.Property(cb => cb.Brand)
        //        .IsRequired()
        //        .HasMaxLength(100);
        //}

        //private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        //{
        //    builder.ToTable("CatalogType");

        //    builder.HasKey(ci => ci.Id);

        //    builder.Property(ci => ci.Id)
        //      // .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
        //       .IsRequired();

        //    builder.Property(cb => cb.Type)
        //        .IsRequired()
        //        .HasMaxLength(100);
        //}

        //private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        //{
        //    var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

        //    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        //    builder.OwnsOne(o => o.ShippingAddress);
        //}

        //private void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder)
        //{

        //}
        /// <summary>
        /// Modify the input SQL query by adding passed parameters
        /// </summary>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>Modified raw SQL query</returns>
        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue;

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                //whether parameter is output
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }

            return sql;
        }
        public new DbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }


        /// <summary>
        /// Generate a script to create all tables for the current model
        /// </summary>
        /// <returns>A SQL script</returns>
        public virtual string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }

        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return this.Query<TQuery>().FromSql(sql);
        }

        /// <summary>
        /// Creates a LINQ query for the entity based on a raw SQL query
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
        {
            return this.Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }

        /// <summary>
        /// Executes the given SQL against the database
        /// </summary>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
        /// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
        /// <param name="parameters">Parameters to use with the SQL</param>
        /// <returns>The number of rows affected</returns>
        public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //set specific command timeout
            var previousTimeout = this.Database.GetCommandTimeout();
            this.Database.SetCommandTimeout(timeout);

            var result = 0;
            if (!doNotEnsureTransaction)
            {
                //use with transaction
                using (var transaction = this.Database.BeginTransaction())
                {
                    result = this.Database.ExecuteSqlCommand(sql, parameters);
                    transaction.Commit();
                }
            }
            else
                result = this.Database.ExecuteSqlCommand(sql, parameters);

            //return previous timeout back
            this.Database.SetCommandTimeout(previousTimeout);

            return result;
        }

        /// <summary>
        /// Detach an entity from the context
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        public virtual void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = this.Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }
    }
}
