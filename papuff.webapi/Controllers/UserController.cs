using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Security;
using papuff.domain.Arguments.Users;
using papuff.domain.Core.Enums;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Linq;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers
{
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
            return Result((UserResponse)Logged);
        }

        [HttpGet("byEmail/{email}")]
        public async Task<IActionResult> ByEmail(string email) {
            var result = await _service.GetByEmail(email);
            return Result((UserResponse)result);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result((UserResponse)result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> List() {
            var result = await _service.ListUsers();
            return Result(result.ToList().ConvertAll(c => (UserResponse)c));
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] AuthRequest request) {
            var result = await _service.Authenticate(request);
            return Result(result);
        }

        [AllowAnonymous]
        [HttpPost("customer")]
        public async Task<IActionResult> Customer([FromBody] UserRequest request) {
            await _service.Create(request, UserType.Customer);
            return Result(new BaseResponse());
        }

        [HttpPost("operator")]
        public async Task<IActionResult> Operator([FromBody] UserRequest request) {

            // todo - pass into filter attribute
            _notify.When<UserController>(Logged.Type == UserType.Root,
                "Você não possui privilégio.");

            await _service.Create(request, UserType.Operator);
            return Result(new BaseResponse());
        }

        [AllowAnonymous]
        [HttpPost("single")]
        public async Task<IActionResult> Single([FromBody] RegisterRequest request){
            await _service.Single(request);
            return Result(new BaseResponse());
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserRequest request) {
            await _service.Update(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }
    }
}