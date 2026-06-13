using MediatR;
using RecruitProApp.Application.Common.Interfaces;

namespace RecruitProApp.Application.JobApplications.Commands.Update
{
    public class UpdateScoreCommandHandler : IRequestHandler<UpdateScoreCommand, Unit>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public UpdateScoreCommandHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<Unit> Handle(UpdateScoreCommand request, CancellationToken cancellationToken)
        {
            var app = await _jobApplicationRepository.GetByIdAsync(request.ApplicationId, cancellationToken)
                ?? throw new KeyNotFoundException("Job application not found.");

            app.SetScore(request.Score);

            await _jobApplicationRepository.UpdateAsync(app, cancellationToken);

            return Unit.Value;
        }
    }
}
