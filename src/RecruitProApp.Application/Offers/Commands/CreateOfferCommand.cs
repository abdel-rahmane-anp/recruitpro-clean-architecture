using MediatR;

namespace RecruitProApp.Application.Offers.Commands
{
    public record CreateOfferCommand(string Title, string Description) : IRequest<Guid>;
}
