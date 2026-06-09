using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class SHAppController : ControllerBase
{
    private readonly ISHAppLogic _shAppLogic;

    public SHAppController(ISHAppLogic shAppLogic)
    {
        _shAppLogic = shAppLogic;
    }

    [HttpGet("dummy")]
    public async Task<ActionResult<IReadOnlyList<DummyItem>>> GetDummyItems()
    {
        var items = await _shAppLogic.GetDummyItemsAsync();
        return Ok(items);
    }

    [HttpGet(Name = "SumMethod")]
    public ActionResult<int> Get(int a, int b)
    {
        int sum = _shAppLogic.Add(a, b);
        return Ok(sum);
    }
}
