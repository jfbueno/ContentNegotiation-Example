using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiation.Api.Controllers;

[ApiController, Route("[controller]")]
public class ContactsController : ControllerBase
{
    [HttpGet("{id}", Name = "GetContactsById")]
    [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]

    public Contact GetById(Guid id) => new(id, "John");

    [HttpGet(Name = "GetContacts")]
    [ProducesResponseType(typeof(Contact[]), StatusCodes.Status200OK)]
    public IEnumerable<Contact> Get() => new Contact[]
    {
        new(Guid.NewGuid(), "John"),
        new(Guid.NewGuid(), "Mark"),
    };
}
