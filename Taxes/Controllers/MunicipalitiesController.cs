using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taxes.Core.Services.Municipalities;
using Taxes.Core.Services.Taxes;
using Taxes.Models.Municipalities;
using Taxes.Models.Taxes;

namespace Taxes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;
        private readonly ITaxService _taxService;

        public MunicipalitiesController(IMunicipalityService municipalityService, ITaxService taxService)
        {
            _municipalityService = municipalityService;
            _taxService = taxService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MunicipalityReadModel), 200)]
        public async Task<IActionResult> Post([FromBody] MunicipalityWriteModel model, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _municipalityService.Create(model, cancellationToken));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(MunicipalityReadModel), 200)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] MunicipalityWriteModel model, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _municipalityService.Update(id, model, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MunicipalityReadModel>), 200)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _municipalityService.List(cancellationToken));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(MunicipalityReadModel), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _municipalityService.GetById(id, cancellationToken));
        }

        [HttpGet]
        [Route("{municipalityId}/tax")]
        [ProducesResponseType(typeof(TaxReadModel), 200)]
        public async Task<IActionResult> GetTax([FromRoute] Guid municipalityId, [FromQuery] DateTime date, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await _taxService.GetTaxForDate(municipalityId, date, cancellationToken));
        }
    }
}
