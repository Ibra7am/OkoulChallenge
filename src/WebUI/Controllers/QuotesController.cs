using Microsoft.AspNetCore.Mvc;
using OkoulChallenge.Application.Dtos;
using OkoulChallenge.Application.Events.Quotes.Commands;
using OkoulChallenge.Application.Events.Quotes.Queries;

namespace WebUI.Controllers;

public class QuotesController : ApiControllerBase
{
    // getRandomQuote, getQuoteByAuthor, addQuote, deleteQuote, updateQuote, listQuotes all of the above methods should have

    [HttpGet]
    public async Task<IList<QuoteDto>> GetAllQuotes()
    {
        return await Mediator.Send(new GetAllQuotes.Query());
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<QuoteDto>> GetRandomQuote()
    {
        return await Mediator.Send(new GetRandomQuote.Query());
    }

    [HttpGet("[action]/{author}")]
    public async Task<IList<QuoteDto>> GetQuoteByAuthor(string author)
    {
        return await Mediator.Send(new GetQuoteByAuthor.Query {Author = author});
    }

    [HttpPost]
    public async Task<int> CreateQuote(CreateQuote.Model model)
    {
        return await Mediator.Send(new CreateQuote.Command {Model = model});
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<QuoteDto>> UpdateQuote(int id, UpdateQuote.Model model)
    {
        return await Mediator.Send(new UpdateQuote.Command {Id = id, Model = model});
    }
    
    [HttpDelete("{id}")]
    public async Task<int> DeleteQuote(int id)
    {
        return await Mediator.Send(new DeleteQuote.Command {Id = id});
    }
}