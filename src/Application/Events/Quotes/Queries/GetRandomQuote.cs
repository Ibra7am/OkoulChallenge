using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Exceptions;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Application.Dtos;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Quotes.Queries;

public class GetRandomQuote
{
    public class Query : IRequest<QuoteDto>
    {
    }

    public class Handler : IRequestHandler<Query, QuoteDto>
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

        public async Task<QuoteDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var randId = Random.Shared.Next(1, _context.Quotes.Count());
            var entity = await _context.Quotes
                .ProjectTo<QuoteDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == randId, cancellationToken: cancellationToken);

            return entity ?? throw new NotFoundException(nameof(Quote), randId);
        }
    }
}