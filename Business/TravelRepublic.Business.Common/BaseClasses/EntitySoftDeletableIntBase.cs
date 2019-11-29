using Eml.Contracts.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelRepublic.Business.Common.BaseClasses
{
    public abstract class EntitySoftDeletableIntBase : EntityIntBase, IEntitySoftdeletableBase
    {
        [Display(Name = "DateDeleted")]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        [Column(Order = 998)]
        public virtual DateTime? DateDeleted { get; set; }

        [MaxLength(255)]
        public string DeletedBy { get; set; }

        [Display(Name = "Reason for deleting:")]
        [DataType(DataType.MultilineText)]
        [Column(Order = 999)]
        public virtual string DeletionReason { get; set; }
    }
}