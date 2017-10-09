using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Eml.SoftDelete;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Contracts.Infrastructure;

namespace TravelRepublic.Data
{
    public class TravelRepublicDb : DbContext
    {
        public TravelRepublicDb():base(ConnectionStrings.TravelRepublicKey)
        {
        }

        public DbSet<Establishment> Establishments { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var conv = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                SoftDeleteColumn.Key,
                (type, attributes) => attributes.Single().SoftDeleteColumnName);

            modelBuilder.Conventions.Add(conv);
            base.OnModelCreating(modelBuilder);
        }
    }
}
