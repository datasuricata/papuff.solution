using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Sieges;
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
    public class CompanyController : BaseController {

        private readonly IServiceCompany _service;

        public CompanyController(IServiceCompany service) {
            _service = service;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me() {
            var result = await _service.GetByUser(LoggedLess);
            return Result(result.ToList().ConvertAll(e =>(CompanyResponse)e));
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result((CompanyResponse)result);
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> ByUser(string id) {
            var result = await _service.GetByUser(id);
            return Result(result.ToList().ConvertAll(e => (CompanyResponse)e));
        }

        [HttpGet("all")]
        public async Task<IActionResult> All() {
            var result = await _service.GetCompanies();
            return Result(result.ToList().ConvertAll(e => (CompanyResponse)e));
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