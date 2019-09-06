using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Generals;
using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Enums;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : BaseController {

        public readonly IServiceUser _service;

        public UserController(IServiceUser service) {
            _service = service;
        }

        [HttpGet("me")]
        public IActionResult Me() {
            return Result(Logged);
        }

        [HttpGet("byEmail/{email}")]
        public async Task<IActionResult> ByEmail(string email) {
            var result = await _service.GetByEmail(email);
            return Result(result);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ByIdAsync(string id) {
            var result = await _service.GetById(id);
            return Result(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllAsync() {
            var result = await _service.ListUsers();
            return Result(result);
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> AuthAsync([FromBody] AuthRequest request) {
            var result = await _service.Authenticate(request);
            return Result(result);
        }

        [AllowAnonymous]
        [HttpPost("register/customer")]
        public async Task<IActionResult> Customer([FromBody] UserRequest request) {
            await _service.Register(request, UserType.Customer);
            return Result(new BaseResponse());
        }

        [HttpPost("register/operator")]
        public async Task<IActionResult> Operator([FromBody] UserRequest request) {

            // todo - pass into filter attribute
            _notify.When<UserController>(Logged.Type == UserType.Root, 
                "Você não possui privilégio.");

            await _service.Register(request, UserType.Operator);
            return Result(new BaseResponse());
        }

        [HttpPost("general")]
        public async Task<IActionResult> General([FromBody] GeneralRequest request) {
            await _service.General(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPost("address")]
        public async Task<IActionResult> Address([FromBody] AddressRequest request) {
            await _service.Address(request.InjectAccount(LoggedLess, nameof(request.OwnerId)));
            return Result(new BaseResponse());
        }

        [HttpPost("wallet")]
        public async Task<IActionResult> Wallet([FromBody] WalletRequest request) {
            await _service.Wallet(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }
    }
}