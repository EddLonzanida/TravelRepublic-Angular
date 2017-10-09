using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eml.SoftDelete;
using TravelRepublic.Contracts.Entities.Core;

namespace TravelRepublic.Business.Common.BaseClasses
{
    [Serializable]
    [SoftDelete(SoftDeleteColumn.Name)]
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Display(Name = "DateDeleted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateDeleted { get; set; }

        [Display(Name = "Reason for deleting:")]
        [DataType(DataType.MultilineText)]
        public string DeletionReason { get; set; }
    }
}
