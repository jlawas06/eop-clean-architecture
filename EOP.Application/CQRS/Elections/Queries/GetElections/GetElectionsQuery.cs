using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Elections.Queries
{
    public class GetElectionsQuery : IRequest<IEnumerable<ElectionDto>>
    {
    }

    internal class GetElectionsQueryHandler : IRequestHandler<GetElectionsQuery, IEnumerable<ElectionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetElectionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ElectionProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<IEnumerable<ElectionDto>> Handle(GetElectionsQuery request, CancellationToken cancellationToken)
        {
            var elections = await _unitOfWork.GetRepository<Election>().GetAllAsync();

            return _mapper.Map<IEnumerable<ElectionDto>>(elections);
        }
    }
}
