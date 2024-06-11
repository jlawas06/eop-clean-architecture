
namespace EOP.Application.Dtos
{
    public class CandidateDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public Guid ElectionId { get; set; }
        public int Votes { get; set; }
    }
}
