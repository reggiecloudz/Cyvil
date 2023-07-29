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
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<ActionItem> ActionItems { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        

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

            builder.Entity<Volunteer>(obj =>
            {
                obj.HasKey(x => new { x.ProjectId, x.UserId });

                obj.HasOne(x => x.Project)
                    .WithMany(a => a.Volunteers)
                    .HasForeignKey(x => x.ProjectId)
                    .IsRequired();

                obj.HasOne(x => x.User)
                    .WithMany(a => a.Participation)
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