using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Application.Dtos;

namespace OkoulChallenge.Application.Events.Quotes.Queries;

public class GetAllQuotes
{
    public class Query : IRequest<IList<QuoteDto>>
    {
    }

    public class Handler : IRequestHandler<Query, IList<QuoteDto>>
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

        public async Task<IList<QuoteDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Quotes
                .ProjectTo<QuoteDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}