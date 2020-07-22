﻿using System;
using System.ComponentModel.DataAnnotations;

using HealthySystem.Data.Contracts;

namespace HealthySystem.Data.Common.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo, IDeletableEntity
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}