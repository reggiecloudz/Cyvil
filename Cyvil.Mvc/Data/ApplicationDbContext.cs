#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cyvil.Mvc.Domain;

namespace Cyvil.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cause> Causes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ProjectParticipant> ProjectParticipants { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<ActionItem> ActionItems { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany(m => m.Projects)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            
            builder.Entity<Assignment>(obj =>
            {
                obj.HasKey(x => new { x.ActionItemId, x.AssigneeId });

                obj.HasOne(x => x.ActionItem)
                    .WithMany(a => a.Assignees)
                    .HasForeignKey(x => x.ActionItemId)
                    .IsRequired();

                obj.HasOne(x => x.Assignee)
                    .WithMany(a => a.Assignments)
                    .HasForeignKey(x => x.AssigneeId)
                    .IsRequired();
            });

            builder.Entity<TeamMember>(obj =>
            {
                obj.HasKey(x => new { x.TeamId, x.MemberId });

                obj.HasOne(x => x.Team)
                    .WithMany(t => t.Members)
                    .HasForeignKey(x => x.TeamId)
                    .IsRequired();

                obj.HasOne(x => x.Member)
                    .WithMany(a => a.Teams)
                    .HasForeignKey(x => x.MemberId)
                    .IsRequired();
            });

            builder.Entity<ProjectParticipant>(obj =>
            {
                obj.HasKey(x => new { x.ProjectId, x.ParticipantId });

                obj.HasOne(x => x.Project)
                    .WithMany(a => a.Participants)
                    .HasForeignKey(x => x.ProjectId)
                    .IsRequired();

                obj.HasOne(x => x.Participant)
                    .WithMany(a => a.Participations)
                    .HasForeignKey(x => x.ParticipantId)
                    .IsRequired();
            });

            builder.Entity<Attendee>(obj =>
            {
                obj.HasKey(x => new { x.MeetingId, x.UserId });

                obj.HasOne(x => x.Meeting)
                    .WithMany(a => a.Attendees)
                    .HasForeignKey(x => x.MeetingId)
                    .IsRequired();

                obj.HasOne(x => x.User)
                    .WithMany(a => a.Meetings)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();
            });

            builder.Entity<MessageRecipient>(obj =>
            {
                obj.HasKey(x => new { x.UserId, x.MessageThreadId });

                obj.HasOne(x => x.MessageThread)
                    .WithMany(x => x.Recipients)
                    .HasForeignKey(x => x.MessageThreadId)
                    .IsRequired();

                obj.HasOne(x => x.User)
                    .WithMany(x => x.Inbox)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();
            });
        }

        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }

            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }
            }
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }

}