using MediatR;
using Microsoft.Extensions.Logging;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.Offers.DTOs;

namespace RecruitProApp.Application.Offers.Queries
{
    public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, List<OfferDto>>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly ILogger<GetOffersQueryHandler> _logger;

        public GetOffersQueryHandler(IOfferRepository offerRepository, ILogger<GetOffersQueryHandler> logger)
        {
            _offerRepository = offerRepository;
            _logger = logger;
        }
        public async Task<List<OfferDto>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetOffersQueryHandler...");

            var offers = await _offerRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation($"Retrivied {offers.Count} offers.");

            return offers.Select(o => 
                new OfferDto(o.Id, o.Title, o.Description, o.PublicationDate))
            .ToList();
        }
    }
}
