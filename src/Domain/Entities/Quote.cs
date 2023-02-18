namespace OkoulChallenge.Domain.Entities;

public class Quote : BaseAuditableEntity
{
    // Data Fields Id: auto-incremented Text: the content of the quote, should allow Unicode CreatedAt: timestamp Author: quote author, linked to author entity

    public string Text { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}