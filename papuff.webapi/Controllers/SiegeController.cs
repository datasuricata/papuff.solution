using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Companies;
using papuff.domain.Arguments.Sieges;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Security;
using papuff.services.Services.Core;
using papuff.webapi.Controllers.Base;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers {
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SiegeController : BaseController {
        private readonly ServiceSiege _service;

        public SiegeController(ServiceSiege service) {
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
        public IActionResult Close(string id) {
            _service.Close(id, LoggedLess);
            return Result(new BaseResponse());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SiegeRequest request) {
            await _service.Create(request.InjectAccount(LoggedLess, nameof(request.OwnerId)));
            return Result(new BaseResponse());
        }

        [HttpPost("receive/ads")]
        public IActionResult Ads([FromBody] AdsRequest request) {
            _service.ReceiveAds(request);
            return Result(new BaseResponse());
        }

        [HttpPost("receive/location")]
        public IActionResult Location([FromBody] LocationRequest request) {
            return Result(_service.ReceiveEntry(request, Logged));
        }
    }
}