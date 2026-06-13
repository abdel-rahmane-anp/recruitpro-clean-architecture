using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitProApp.Application.Candidates.Commands;
using RecruitProApp.Application.Candidates.Queries;

namespace RecruitProApp.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CandidateAppController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateAppController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _mediator.Send(new GetCandidatesQuery());
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var candidate = await _mediator.Send(new GetCandidateByIdQuery(id));
            return candidate is null ? NotFound() : Ok(candidate);
        }
    }
}
