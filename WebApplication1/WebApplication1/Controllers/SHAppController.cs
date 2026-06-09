using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SHAppController : ControllerBase
{
    private readonly SHAppLogic _shAppLogic;

    public SHAppController(SHAppLogic shAppLogic)
    {
        _shAppLogic = shAppLogic;
    }

    [HttpGet("dummy")]
    public async Task<ActionResult<IReadOnlyList<DummyItem>>> GetDummyItems()
    {
        var items = await _shAppLogic.GetDummyItemsAsync();
        return Ok(items);
    }

    [HttpGet("sum")]
    public ActionResult<int> GetSum(int a, int b)
    {
        int sum = _shAppLogic.Add(a, b);
        return Ok(sum);
    }

    [HttpGet("multiple")]
    public ActionResult<int> Getmultiple(int a, int b)
    {
        int sum = _shAppLogic.multiple(a, b);
        return Ok(sum);
    }
}
