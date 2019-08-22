using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Companies;
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
        [HttpPost("register/customer")]
        public async Task<IActionResult> Customer([FromBody] UserRequest request) {
            await _service.Register(request, UserType.Customer);
            return Result(new BaseResponse());
        }

        [HttpPost("register/operator")]
        public async Task<IActionResult> Operator([FromBody] UserRequest request) {

            // todo pass to filter
            Notifier.When<UsersController>(Logged.Type == UserType.Root, 
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
            await _service.Address(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPost("wallet")]
        public async Task<IActionResult> Wallet([FromBody] WalletRequest request) {
            await _service.Wallet(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        //[HttpPost("company")]
        //public async Task<IActionResult> Company([FromBody] CompanyRequest request) {
        //    throw NotImplemented
        //}
    }
}