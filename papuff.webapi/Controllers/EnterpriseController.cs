using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Arguments.Users;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EnterpriseController : BaseController {

        private readonly IServiceCompany _service;

        public EnterpriseController(IServiceCompany service) {
            _service = service;
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result(result);
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> ByUser(string id) {
            var result = await _service.GetByUser(id);
            return Result(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> All() {
            var result = await _service.GetCompanies();
            return Result(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CompanyRequest request) {
            await _service.Create(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPost("address/{id}")]
        public async Task<IActionResult> Address(string id, [FromBody] AddressRequest request) {
            await _service.Address(request);
            return Result(new BaseResponse());
        }
    }
}