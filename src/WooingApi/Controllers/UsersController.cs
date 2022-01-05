using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WooingApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;

            return Ok("Hai");   
        }
    }
}