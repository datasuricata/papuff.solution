using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SiegeController : BaseController {
        private readonly IServiceSiege _service;

        public SiegeController(IServiceSiege service) {
            _service = service;
        }

        [HttpGet("byId/{id}")]
        public IActionResult ById(string id) {
            return Result(_service.GetById(id));
        }

        [HttpGet("all")]
        public IActionResult All() {
            return Result(_service.ListSieges());
        }

        [HttpGet("close/{id}")]
        public async Task<IActionResult> Close(string id) {
            await _service.Close(id, LoggedLess);
            return Result(new BaseResponse());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SiegeRequest request) {
            await _service.Register(request.InjectAccount(LoggedLess, nameof(request.OwnerId)));
            return Result(new BaseResponse());
        }

        [HttpPost("receive")]
        public IActionResult Receive([FromBody] LocationRequest request) {
            return Result(_service.ReceiveEntry(request, Logged));
        }
    }
}