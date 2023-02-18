using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Application.Common.Mappings;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Quotes.Commands
{
    public class CreateQuote
    {
        public class Command : IRequest<int>
        {
            public Model Model { get; set; }
        }

        public class Model : IMapFrom<Quote>
        {
            public string Text { get; set; }
            public string AuthorName { get; set; }

            // public void Mapping(Profile profile)
            // {
            //     profile.CreateMap<Event, Model>().ReverseMap();
            // }
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
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == request.Model.AuthorName, cancellationToken: ct);
                var entity = new Quote {Text = request.Model.Text, Author = author ?? new Author {Name = request.Model.AuthorName}};

                _context.Quotes.Add(entity);
                await _context.SaveChangesAsync(ct);
                return entity.Id;
            }
        }
    }
}