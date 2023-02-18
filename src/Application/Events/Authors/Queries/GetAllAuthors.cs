using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Application.Dtos;

namespace OkoulChallenge.Application.Events.Authors.Queries;

public class GetAllAuthors
{
    public class Query : IRequest<IList<AuthorDto>>
    {
    }
    
    public class Handler : IRequestHandler<Query, IList<AuthorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IList<AuthorDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Authors
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}