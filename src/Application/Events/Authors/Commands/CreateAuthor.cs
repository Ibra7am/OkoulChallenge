using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OkoulChallenge.Application.Common.Exceptions;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Authors.Commands;

public class CreateAuthor
{
    public class Command : IRequest<int>
    {
        public Model Model { get; set; }
    }

    public class Model
    {
        public string Name { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.Model.Name)
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
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name == request.Model.Name, cancellationToken: ct);
            if (existingAuthor != null)
            {
                throw new ForbiddenAccessException();
            }

            var entity = new Author {Name = request.Model.Name};
            _context.Authors.Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity.Id;
        }
    }
}