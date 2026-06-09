using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SHAppController : ControllerBase
    {
        private readonly SHAppLogic _shAppLogic;
        public SHAppController(SHAppLogic shAppLogic)
        {
            _shAppLogic = shAppLogic;
        }
        [HttpGet(Name = "SumMethod")]
        public int Get(int a, int b)
        {
            int sum = _shAppLogic.Add(a, b);
            Console.WriteLine($"The sum of {a} and {b} is: {sum}");
            return sum;
        }
    }
}
