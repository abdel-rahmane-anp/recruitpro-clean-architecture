using MediatR;
using RecruitProApp.Application.Offers.DTOs;

namespace RecruitProApp.Application.Offers.Queries
{
    public record GetOffersQuery() : IRequest<List<OfferDto>>;
}
