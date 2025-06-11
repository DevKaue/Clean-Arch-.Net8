using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public  class Card
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? Title { get; set; }

        [StringLength(120)]
        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeadLine { get; set; }

        public ListCard? List { get; set; }

        public StatusCardEnum Status { get; set; }
    }
}
