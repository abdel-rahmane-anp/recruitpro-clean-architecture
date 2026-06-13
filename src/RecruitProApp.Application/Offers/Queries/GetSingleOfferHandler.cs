using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.Offers.DTOs;

namespace RecruitProApp.Application.Offers.Queries
{
    public class GetSingleOfferHandler : IRequestHandler<GetSingleOfferQuery, OfferDto?>
    {
        private readonly IOfferRepository _offerRepository;

        public GetSingleOfferHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public async Task<OfferDto?> Handle(GetSingleOfferQuery request, CancellationToken cancellationToken)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(request.Id, cancellationToken);

            if (offer == null) return null;

            return new OfferDto(offer.Id, offer.Title, offer.Description, offer.PublicationDate);
        }
    }
}
