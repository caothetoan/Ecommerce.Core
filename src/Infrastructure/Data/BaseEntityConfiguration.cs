using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vnit.ApplicationCore.Entities;

namespace Vnit.Infrastructure.Data
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        protected virtual string TableName { get { return typeof(T).Name; } }

      
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(this.TableName);
        }
    }
}
