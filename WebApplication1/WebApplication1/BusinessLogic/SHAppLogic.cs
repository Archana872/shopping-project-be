using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.BusinessLogic
{
    [ApiController]
    [Route("[controller]")]
    public class SHAppLogic 
    {
        public int SUM Add(int a , int b)
        {
            int sum = a + b;
            return sum;
        }
    }
}
