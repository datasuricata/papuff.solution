using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Generals;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GeneralController : BaseController {

        public readonly IServiceGeneral _service;

        public GeneralController(IServiceGeneral service) {
            _service = service;
        }

        [HttpGet("me")]
        public async Task<IActionResult> ById() {
            var result = await _service.GetByUser(LoggedLess);
            return Result((GeneralResponse)result);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result((GeneralResponse)result);
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> ByUser(string id) {
            var result = await _service.GetByUser(id);
            return Result((GeneralResponse)result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> General([FromBody] GeneralRequest request) {
            await _service.General(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }
    }
}