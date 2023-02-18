using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Exceptions;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Application.Dtos;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Quotes.Queries;

public class GetQuoteByAuthor
{
    public class Query : IRequest<IList<QuoteDto>>
    {
        public string Author { get; set; }
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
            var entities = await _context.Quotes
                .Where(q => q.Author.Name.Contains(request.Author))
                .ProjectTo<QuoteDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (entities.Count <= 0)
            {
                throw new NotFoundException(nameof(Quote));
            }

            return entities;
        }
    }
}