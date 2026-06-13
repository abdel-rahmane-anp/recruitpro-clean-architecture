using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Application.Common.Interfaces
{
    public interface IOfferRepository
    {
        Task AddAsync(Offer offer, CancellationToken cancellationToken);
        Task<Offer?> GetOfferByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Offer>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
