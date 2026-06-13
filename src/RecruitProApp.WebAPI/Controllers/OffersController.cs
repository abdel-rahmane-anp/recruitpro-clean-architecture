using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitProApp.Application.Offers.Commands;
using RecruitProApp.Application.Offers.Queries;

namespace RecruitProApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOfferCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllOffers()
        {
            var offers =  await _mediator.Send(new GetOffersQuery());
            return Ok(offers);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var offer = await _mediator.Send(new GetSingleOfferQuery(id));
            return Ok(offer);
        }
    }
}
