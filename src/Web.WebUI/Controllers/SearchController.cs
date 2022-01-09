using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Search.Queries.Search;

namespace Web.WebUI.Controllers;
public class SearchController: ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}