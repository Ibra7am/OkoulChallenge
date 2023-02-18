using Microsoft.AspNetCore.Mvc;
using OkoulChallenge.Application.Dtos;
using OkoulChallenge.Application.Events.Authors.Commands;
using OkoulChallenge.Application.Events.Authors.Queries;

namespace WebUI.Controllers;

public class AuthorsController : ApiControllerBase
{
    // addAuthor, deleteAuthor, UpdateAuthor, listAuthors

    [HttpGet]
    public async Task<IList<AuthorDto>> GetAllAuthors()
    {
        return await Mediator.Send(new GetAllAuthors.Query());
    }

    [HttpPost]
    public async Task<int> CreateAuthor(CreateAuthor.Model model)
    {
        return await Mediator.Send(new CreateAuthor.Command {Model = model});
    }

    [HttpPut("{id}")]
    public async Task<int> UpdateAuthor(int id, UpdateAuthor.Model model)
    {
        return await Mediator.Send(new UpdateAuthor.Command {Id = id, Model = model});
    }
    
    [HttpDelete("{id}")]
    public async Task<int> DeleteAuthor(int id)
    {
        return await Mediator.Send(new DeleteAuthor.Command {Id = id});
    }
}