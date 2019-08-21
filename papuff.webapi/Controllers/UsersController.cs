using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Generals;
using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : BaseController {

        public readonly IServiceUser _service;

        public UsersController(IServiceUser service) {
            _service = service;
        }

        [HttpGet("me")]
        public IActionResult Me() {
            return Result(Logged);
        }

        [HttpGet("byEmail/{email}")]
        public IActionResult ByEmail(string email) {
            return Result(_service.GetByEmail(email));
        }

        [HttpGet("byId/{id}")]
        public IActionResult ById(string id) {
            return Result(_service.GetById(id));
        }

        [HttpGet("all")]
        public IActionResult All() {
            return Result(_service.ListUsers());
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Auth([FromBody] AuthRequest request) {
            return Result(_service.Authenticate(request));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest request) {
            await _service.Register(request);
            return Result(new BaseResponse());
        }

        [HttpPost("general")]
        public async Task<IActionResult> General([FromBody] GeneralRequest request) {
            await _service.General(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPost("address")]
        public async Task<IActionResult> Address([FromBody] AddressRequest request) {
            await _service.Address(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPost("wallet")]
        public async Task<IActionResult> Wallet([FromBody] WalletRequest request) {
            await _service.Wallet(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }
    }
}