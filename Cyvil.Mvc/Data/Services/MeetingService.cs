using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Data.Services
{
    public class MeetingService :  IMeetingService
    {
        private readonly ApplicationDbContext _context;
        public MeetingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InviteeListModel>> GetInvitees(int eventType, long id)
        {
            switch (eventType)
            {
                case 1:
                    var participants = await _context.ProjectParticipants
                        .Where(p => p.ProjectId == id)
                        .Include(p => p.Participant)
                        .Select(p => new InviteeListModel
                        { 
                            Id = p.ParticipantId,
                            Name = p.Participant!.FullName,
                            Role = p.Position
                        })
                        .ToListAsync();
                    return participants;
                case 2:
                    var applicants = await _context.Applicants
                        .Where(a => a.PositionId == id && a.ApplicantStatus == ApplicantStatus.Interview)
                        .Include(a => a.User)
                        .Select(a => new InviteeListModel
                        {
                            Id = a.UserId,
                            Name = a.User!.FullName
                        })
                        .ToListAsync();
                    return applicants;
                case 3:
                    var members = await _context.TeamMembers
                        .Where(t => t.TeamId == id)
                        .Include(t => t.Member)
                        .Select(t => new InviteeListModel
                        {
                            Id = t.MemberId,
                            Name = t.Member!.FullName,
                            Role = t.Role
                        })
                        .ToListAsync();
                    return members;
                case 4:
                    var invitees = new List<InviteeListModel>();
                    var actionItem = await _context.ActionItems
                        .Include(a => a.Team)
                            .ThenInclude(t => t!.Members)
                                .ThenInclude(m => m.Member)
                        .FirstOrDefaultAsync(a => a.Id == id);
                    foreach (var item in actionItem!.Team!.Members)
                    {
                        invitees.Add(new InviteeListModel
                        {
                            Id = item.MemberId,
                            Name = item.Member!.FullName,
                            Role = item.Role
                        });
                    }
                    return invitees;
                default:
                    return new List<InviteeListModel>();
            }
        }

        // Implement specific methods for the Category repository here.

        // Usage example:
        // var dbContext = new YourDbContext();
        // var categoryRepository = new CategoryRepository(dbContext);
        // var categories = await categoryRepository.GetAllAsync();
        // var category = await categoryRepository.GetByIdAsync(1);
        // categoryRepository.AddAsync(new Category { Name = "New Category" });
        // categoryRepository.Update(category);
        // categoryRepository.Remove(category);
        // await dbContext.SaveChangesAsync();

    }
}