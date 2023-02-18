using AutoMapper;
using FluentValidation;
using MediatR;
using OkoulChallenge.Application.Common.Exceptions;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Quotes.Commands
{
    public class DeleteQuote
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Id)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, int>
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

            public async Task<int> Handle(Command request, CancellationToken ct)
            {
                var entity = await _context.Quotes.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Quote), request.Id);
                }

                _context.Quotes.Remove(entity);
                await _context.SaveChangesAsync(ct);
                
                return entity.Id;
            }
        }
    }
}