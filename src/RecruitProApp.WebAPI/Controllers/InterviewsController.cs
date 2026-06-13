using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitProApp.Application.Interviews.Commands.CancelInterview;
using RecruitProApp.Application.Interviews.Commands.RescheduleInterview;
using RecruitProApp.Application.Interviews.Commands.ScheduleInterview;
using RecruitProApp.Application.Interviews.Queries.Get;

namespace RecruitProApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Schedule([FromBody] ScheduleInterviewCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByApplication), new { jobApplicationId = command.JobApplicationId }, id);
        }

        [HttpGet("by-application/{jobApplicationId}")]
        public async Task<IActionResult> GetByApplication([FromRoute] Guid jobApplicationId)
        {
            var result = await _mediator.Send(new GetInterviewsByApplicationIdQuery(jobApplicationId));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _mediator.Send(new CancelInterviewCommand(id));
            return NoContent();
        }

        [HttpPut("{id}/reschedule")]
        public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleInterviewCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
