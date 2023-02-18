namespace OkoulChallenge.Domain.Entities;

public class Author : BaseAuditableEntity
{
    // Id: auto-incremented
    // Name: author's name
    // CreatedAt: timestamp

    public string Name { get; set; }
    public IList<Quote> Quotes { get; private set; } = new List<Quote>();
}