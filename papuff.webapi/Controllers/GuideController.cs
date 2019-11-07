using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Generals;
using papuff.domain.Interfaces.Services.Core;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GuideController : BaseController {

        public readonly IServiceGuide _service;

        public GuideController(IServiceGuide service) {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GuideRequest request) {
            await _service.Create(request);
            return Result(new BaseResponse());
        }
    }
}