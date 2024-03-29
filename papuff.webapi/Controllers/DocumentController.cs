﻿using Microsoft.AspNetCore.Authorization;
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
    public class DocumentController : BaseController {

        public readonly IServiceDocument _service;

        public DocumentController(IServiceDocument service) {
            _service = service;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me() {
            var result = await _service.GetByUser(LoggedLess);
            return Result(result.ToList().ConvertAll(e => (DocumentResponse)e));
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> ById(string id) {
            var result = await _service.GetById(id);
            return Result((DocumentResponse)result);
        }

        [HttpGet("byUser/{id}")]
        public async Task<IActionResult> ByUser(string id) {
            var result = await _service.GetByUser(id);
            return Result(result.ToList().ConvertAll(e => (DocumentResponse)e));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DocumentRequest request) {
            await _service.Create(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] DocumentRequest request) {
            await _service.Update(request.InjectAccount(LoggedLess, nameof(request.UserId)));
            return Result(new BaseResponse());
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id) {
            await _service.Delete(id);
            return Result(new BaseResponse());
        }

        [HttpPut("padLock/{id}")]
        public async Task<IActionResult> PadLock(string id) {
            await _service.PadLock(id);
            return Result(new BaseResponse());
        }
    }
}