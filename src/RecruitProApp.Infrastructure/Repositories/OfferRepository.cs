using Microsoft.EntityFrameworkCore;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.Offers.DTOs;
using RecruitProApp.Domain.Entities.Offers;
using RecruitProApp.Infrastructure.Persistence;

namespace RecruitProApp.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly RecruitProAppDbContext _context;

        public OfferRepository(RecruitProAppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Offer offer, CancellationToken cancellationToken)
        {
            _context.Offers.Add(offer);
            await this.SaveChanges(cancellationToken);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Offers.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Offer>> GetAllAsync(CancellationToken cancellationToke)
        {
            return await _context.Offers
                .ToListAsync(cancellationToke);
        }

        public async Task<Offer?> GetOfferByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Offers
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SaveChanges(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
