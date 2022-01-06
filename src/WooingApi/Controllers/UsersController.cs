using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WooingApi.Controllers
{
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