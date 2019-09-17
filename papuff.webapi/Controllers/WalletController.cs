using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Users;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Linq;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WalletController : BaseController {

        public readonly IServiceWallet _service;

        public WalletController(IServiceWallet service) {
            _service = service;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me() {
            var result = await _service.GetByUser(LoggedLess);
            return Result(result.ToList().ConvertAll(e => (WalletResponse)e));
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result((WalletResponse)result);
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> ByUser(string id) {
            var result = await _service.GetByUser(id);
            return Result(result.ToList().ConvertAll(e => (WalletResponse)e));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] WalletRequest request) {
            await _service.Wallet(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }
    }
}