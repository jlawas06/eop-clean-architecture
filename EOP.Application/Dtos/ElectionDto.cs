using EOP.Domain.Enumerations;

namespace EOP.Application.Dtos
{
    public class ElectionDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ElectionStatusEnum Status { get; set; }
        public ICollection<CandidateDto> Candidates { get; set; }
    }
}
