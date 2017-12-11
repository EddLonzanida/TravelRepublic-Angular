using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Eml.DataRepository;
using Eml.SoftDelete;
using TravelRepublic.Business.Common.Entities;
using TravelRepublic.Contracts.Infrastructure;

namespace TravelRepublic.Data
{
    public class TravelRepublicDb : DbContext, IAllowIdentityInsertWhenSeeding
    {
        public bool AllowIdentityInsertWhenSeeding { get; set; }

        public DbSet<Establishment> Establishments { get; set; }

        public TravelRepublicDb():base(ConnectionStrings.TravelRepublicKey)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var conv = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                SoftDeleteColumn.Key,
                (type, attributes) => attributes.Single().SoftDeleteColumnName);

            modelBuilder.Conventions.Add(conv);

            if (AllowIdentityInsertWhenSeeding)
            {
                modelBuilder.Properties<int>().Where(r => r.Name.Equals("Id"))
                    .Configure(r => r.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None));
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
