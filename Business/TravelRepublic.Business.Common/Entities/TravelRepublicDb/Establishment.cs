using Eml.Contracts.Entities;
using TravelRepublic.Business.Common.BaseClasses;
using TravelRepublic.Infrastructure.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TravelRepublic.Business.Common.Entities.TravelRepublicDb
{
    public class Establishment : EntitySoftDeletableIntBase, ISearchableName, ITravelRepublicDbEntity
    {
        public int EstablishmentId { get; set; }

        public double Distance { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public int Stars { get; set; }

        public string EstablishmentType { get; set; }

        public double UserRating { get; set; }

        public string UserRatingTitle { get; set; }

        public int UserRatingCount { get; set; }

        [DataType(DataType.Url)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Url)]
        public string ThumbnailUrl { get; set; }

        public double MinCost { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string SearchableName => Name;
    }
}