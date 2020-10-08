using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taxes.Core.Services.Taxes;
using Taxes.Models.Taxes;

namespace Taxes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxesController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxesController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaxReadModel), 200)]
        public async Task<IActionResult> Post([FromBody]TaxWriteModel model, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _taxService.Create(model, cancellationToken));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(TaxReadModel), 200)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] TaxWriteModel model, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _taxService.Update(id, model, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaxReadModel>), 200)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _taxService.List(cancellationToken));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(TaxReadModel), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _taxService.GetById(id, cancellationToken));
        }
    }
}