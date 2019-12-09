﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelRepublic.DataMigration.TravelRepublicDbMigrations
{
    [DbContext(typeof(TravelRepublicDb))]
    partial class TravelRepublicDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TravelRepublic.Business.Common.Entities.TravelRepublicDb.Establishment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateDeleted");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(255);

                    b.Property<string>("DeletionReason");

                    b.Property<double>("Distance");

                    b.Property<int>("EstablishmentId");

                    b.Property<string>("EstablishmentType");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Location");

                    b.Property<double>("MinCost");

                    b.Property<string>("Name");

                    b.Property<int>("Stars");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<double>("UserRating");

                    b.Property<int>("UserRatingCount");

                    b.Property<string>("UserRatingTitle");

                    b.HasKey("Id");

                    b.ToTable("Establishments");
                });
#pragma warning restore 612, 618
        }
    }
}
