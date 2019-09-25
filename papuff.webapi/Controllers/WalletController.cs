using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using papuff.domain.Arguments.Base;
using papuff.domain.Arguments.Users;
using papuff.domain.Interfaces.Services.Core;
using papuff.webapi.Controllers.Base;
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
            return Result((WalletResponse)result);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result((WalletResponse)result);
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> ByUser(string id) {
            var result = await _service.GetByUser(id);
            return Result((WalletResponse)result);
        }

        [HttpGet("wallet/{id}")]
        public async Task<IActionResult> WalletCreate(string id) {
            await _service.Wallet(id);
            return Result(new BaseResponse());
        }

        [HttpPost("payment/{id}/create")]
        public async Task<IActionResult> PaymentCreate(string id, [FromBody] PaymentRequest request) {
            await _service.PaymentCreate(id, request);
            return Result(new BaseResponse());
        }

        [HttpPost("receipt/{id}/update")]
        public async Task<IActionResult> ReceiveUpdate(string id, [FromBody] ReceiptRequest request) {
            await _service.Receipt(id, request);
            return Result(new BaseResponse());
        }
    }
}