using EOP.Domain.Common;
using EOP.Domain.Enumerations;

namespace EOP.Domain.Entities
{
    public class Election : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ElectionStatusEnum Status { get; set; } = ElectionStatusEnum.NotStarted;
        public ICollection<Candidate> Candidates { get; set; }
    }
}
