using OkoulChallenge.Application.Common.Mappings;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Dtos;

public class AuthorDto : IMapFrom<Author>
{
    public string Name { get; set; }
    public IList<QuoteDto> Quotes { get; private set; } = new List<QuoteDto>();
}