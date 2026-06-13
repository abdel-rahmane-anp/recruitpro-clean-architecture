using MediatR;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities;
using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Application.Offers.Commands
{
    public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, Guid>
    {
        private readonly IOfferRepository _offerRepository;

        public CreateOfferHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<Guid> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offerToCreate = new Offer(request.Title, request.Description);
            await _offerRepository.AddAsync(offerToCreate, cancellationToken);
            return offerToCreate.Id;
        }
    }
}
