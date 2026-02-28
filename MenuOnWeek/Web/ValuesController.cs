using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuOnWeek.Web
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int GetValue()
        {
            return 0; 
        }
    }
}
