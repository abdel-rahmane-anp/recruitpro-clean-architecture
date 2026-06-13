using MediatR;
using Microsoft.EntityFrameworkCore;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Common;
using RecruitProApp.Domain.Entities.Candidates;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.Offers;
using RecruitProApp.Domain.ValueObjects;

namespace RecruitProApp.Infrastructure.Persistence
{
    public class RecruitProAppDbContext : DbContext, IRecruitProAppDbContext
    {
        private readonly IPublisher _publisher;

        public RecruitProAppDbContext(DbContextOptions<RecruitProAppDbContext> options, IPublisher publisher)
            : base(options)
        {
            _publisher = publisher;
        }

        public DbSet<Offer> Offers => Set<Offer>();
        public DbSet<JobApplication> JobApplications => Set<JobApplication>();
        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<Interview> Interviews => Set<Interview>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Collect the domain events tracked on aggregates before saving.
            var aggregates = ChangeTracker
                .Entries<AggregateRoot>()
                .Select(entry => entry.Entity)
                .Where(aggregate => aggregate.DomainEvents.Count > 0)
                .ToList();

            var domainEvents = aggregates.SelectMany(aggregate => aggregate.DomainEvents).ToList();

            // Persist first, then dispatch events (so handlers observe committed state).
            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            aggregates.ForEach(aggregate => aggregate.ClearDomainEvents());

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Title).IsRequired().HasMaxLength(200);
                entity.Property(o => o.Description).IsRequired();
                entity.Property(o => o.PublicationDate).IsRequired();
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(100);

                // Value object <-> int column (no schema change).
                entity.Property(e => e.Score)
                      .HasConversion(score => score.Value, value => new Score(value));

                entity.HasOne(e => e.Offer)
                      .WithMany(o => o.JobApplications)
                      .HasForeignKey(e => e.OfferId);

                entity.HasOne(e => e.Candidate)
                      .WithMany(c => c.JobApplications)
                      .HasForeignKey(e => e.CandidateId);
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(100);

                // Value object <-> nvarchar column (no schema change).
                entity.Property(e => e.Email)
                      .HasConversion(email => email.Value, value => new Email(value))
                      .HasMaxLength(256)
                      .IsRequired();
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Link).HasMaxLength(255);
                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.HasOne(i => i.JobApplication)
                      .WithMany(j => j.Interviews)
                      .HasForeignKey(i => i.JobApplicationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
