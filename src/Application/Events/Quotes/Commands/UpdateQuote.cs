using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Exceptions;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Application.Dtos;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Quotes.Commands
{
    public class UpdateQuote
    {
        public class Command : IRequest<QuoteDto>
        {
            public int Id { get; set; }
            public Model Model { get; set; }
        }

        public class Model
        {
            public string Text { get; set; }
            public string AuthorName { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Model.Text)
                    .MinimumLength(2)
                    .NotEmpty();

                RuleFor(r => r.Model.AuthorName)
                    .MinimumLength(2)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, QuoteDto>
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

            public async Task<QuoteDto> Handle(Command request, CancellationToken ct)
            {
                var entity = await _context.Quotes.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Quote), request.Id);
                }
                
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == request.Model.AuthorName, cancellationToken: ct);
                entity.Text = request.Model.Text;
                entity.Author = author ?? new Author {Name = request.Model.AuthorName};

                await _context.SaveChangesAsync(ct);
                return _mapper.Map<QuoteDto>(entity);
            }
        }
    }
}