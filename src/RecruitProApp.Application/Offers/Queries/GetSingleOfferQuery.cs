using MediatR;
using RecruitProApp.Application.Offers.DTOs;

namespace RecruitProApp.Application.Offers.Queries
{
    public record GetSingleOfferQuery(Guid Id) : IRequest<OfferDto?>;
}
