using EOP.Domain.Common;

namespace EOP.Domain.Entities
{
    public class Candidate : BaseEntity
    {
        public required string Name { get; set; }
        public Guid ElectionId { get; set; }
        public Election Election { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
