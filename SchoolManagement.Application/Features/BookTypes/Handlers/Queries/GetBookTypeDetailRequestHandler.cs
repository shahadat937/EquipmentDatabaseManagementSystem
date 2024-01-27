using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BookType;
using SchoolManagement.Application.Features.BookTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BookTypes.Handlers.Queries
{
    public class GetBookTypeDetailRequestHandler : IRequestHandler<GetBookTypeDetailRequest, BookTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<BookType> _BookTypeRepository;
        public GetBookTypeDetailRequestHandler(ISchoolManagementRepository<BookType> BookTypeRepository, IMapper mapper)
        {
            _BookTypeRepository = BookTypeRepository;
            _mapper = mapper;
        }
        public async Task<BookTypeDto> Handle(GetBookTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var BookType = await _BookTypeRepository.Get(request.BookTypeId);
            return _mapper.Map<BookTypeDto>(BookType);
        }
    }
}
