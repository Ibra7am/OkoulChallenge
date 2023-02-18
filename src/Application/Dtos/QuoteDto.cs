using OkoulChallenge.Application.Common.Mappings;
using OkoulChallenge.Domain.Entities;

namespace OkoulChallenge.Application.Dtos;

public class QuoteDto : IMapFrom<Quote>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public AuthorDto Author { get; set; }
    public int AuthorId { get; set; }
}