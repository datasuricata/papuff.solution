using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Security;
using papuff.domain.Interfaces.Services.Core;
using papuff.webapi.Controllers.Base;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController {

        public readonly IServiceUser service;

        public UsersController(IServiceUser service) {
            this.service = service;
        }

        [HttpPost("auth")]
        public IActionResult Auth([FromBody] AuthRequest request) {
            return Result(service.Authenticate(request));
        }
    }
}
