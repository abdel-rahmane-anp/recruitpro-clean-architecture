using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitProApp.Application.JobApplications.Commands.AcceptJobApplication;
using RecruitProApp.Application.JobApplications.Commands.CreateJobApplication;
using RecruitProApp.Application.JobApplications.Commands.RejectApplication;
using RecruitProApp.Application.JobApplications.Commands.Update;
using RecruitProApp.Application.JobApplications.Queries;

namespace RecruitProApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobApplicationCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetJobApplicationsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetJobApplicationByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            await _mediator.Send(new AcceptJobApplicationCommand(id));
            return NoContent();
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id, [FromBody] RejectRequest request)
        {
            await _mediator.Send(new RejectJobApplicationCommand(id, request.Reason));
            return NoContent();
        }

        [HttpPut("{id}/score")]
        public async Task<IActionResult> Score(Guid id, [FromBody] UpdateScoreCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }

}
