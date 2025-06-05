using Domain.Enum;

namespace Domain.Entity
{
    public  class Card
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeadLine { get; set; }
        public ListCard? List { get; set; }
        public StatusCardEnum Status { get; set; }
    }
}
