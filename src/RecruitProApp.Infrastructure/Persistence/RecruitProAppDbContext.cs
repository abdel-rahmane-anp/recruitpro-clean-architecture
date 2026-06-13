using Microsoft.EntityFrameworkCore;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Domain.Entities.Candidates;
using RecruitProApp.Domain.Entities.Interviews;
using RecruitProApp.Domain.Entities.JobApplications;
using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Infrastructure.Persistence
{
    public class RecruitProAppDbContext : DbContext, IRecruitProAppDbContext
    {
        public RecruitProAppDbContext(DbContextOptions<RecruitProAppDbContext> options)
            : base(options)
        { }

        public DbSet<Offer> Offers => Set<Offer>();
        public DbSet<JobApplication> JobApplications => Set<JobApplication>();
        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<Interview> Interviews => Set<Interview>();

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

                // Relation avec Offer
                entity.HasOne(e => e.Offer)
                      .WithMany(o => o.JobApplications)
                      .HasForeignKey(e => e.OfferId);

                // Relation avec Candidate
                entity.HasOne(e => e.Candidate)
                      .WithMany(c => c.JobApplications)
                      .HasForeignKey(e => e.CandidateId);
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(100);
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Link).HasMaxLength(255);
                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity
                    .HasOne(i => i.JobApplication)
                    .WithMany(j => j.Interviews)
                    .HasForeignKey(i => i.JobApplicationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
