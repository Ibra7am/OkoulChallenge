using AutoMapper;
using MediatR;
using OkoulChallenge.Application.Common.Exceptions;
using OkoulChallenge.Application.Common.Interfaces;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Events.Authors.Commands;

public class DeleteAuthor
{
    public class Command : IRequest<int>
    {
        public int Id { get; set; }
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
            var entity = await _context.Authors.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Author), request.Id);
            }

            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync(ct);
            
            return entity.Id;
        }
    }
}