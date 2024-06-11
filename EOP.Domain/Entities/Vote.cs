using EOP.Domain.Common;

namespace EOP.Domain.Entities
{
    public class Vote : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CandidateId { get; set; }
        public User User { get; set; }
        public Candidate Candidate { get; set; }
    }
}
