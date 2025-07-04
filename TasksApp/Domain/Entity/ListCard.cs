﻿using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class ListCard
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? Title { get; set; }

        public StatusItemEnum Status { get; set; } = StatusItemEnum.Active;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Workspace? Workspace { get; set; }

        public ICollection<Card>? Cards { get; set; } 
    }
}