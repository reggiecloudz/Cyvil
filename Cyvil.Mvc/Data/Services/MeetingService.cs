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

        public async Task<List<SelectableUserModel>> GetInvitees(int eventType, long id)
        {
            var attendees = await _context.Attendees
                .Where(m => m.MeetingId == id)
                .Select(m => m.UserId)
                .ToListAsync();

            switch (eventType)
            {
                case 1:
                    var participants = await _context.ProjectParticipants
                        .Where(p => p.ProjectId == id)
                        .Include(p => p.Participant)
                        .Select(p => new SelectableUserModel
                        { 
                            Id = p.ParticipantId,
                            Name = p.Participant!.FullName,
                            Role = p.Position,
                            Selected = attendees.Contains(p.ParticipantId)
                        })
                        .ToListAsync();
                    return participants;
                case 2:
                    var applicants = await _context.Applicants
                        .Where(a => a.PositionId == id && a.ApplicantStatus == ApplicantStatus.Interview)
                        .Include(a => a.User)
                        .Select(a => new SelectableUserModel
                        {
                            Id = a.UserId,
                            Name = a.User!.FullName,
                            Selected = attendees.Contains(a.UserId)
                        })
                        .ToListAsync();
                    return applicants;
                case 3:
                    var members = await _context.TeamMembers
                        .Where(t => t.TeamId == id)
                        .Include(t => t.Member)
                        .Select(t => new SelectableUserModel
                        {
                            Id = t.MemberId,
                            Name = t.Member!.FullName,
                            Role = t.Role,
                            Selected = attendees.Contains(t.MemberId)
                        })
                        .ToListAsync();
                    return members;
                case 4:
                    var invitees = new List<SelectableUserModel>();
                    var actionItem = await _context.ActionItems
                        .Include(a => a.Team)
                            .ThenInclude(t => t!.Members)
                                .ThenInclude(m => m.Member)
                        .FirstOrDefaultAsync(a => a.Id == id);
                    foreach (var item in actionItem!.Team!.Members)
                    {
                        invitees.Add(new SelectableUserModel
                        {
                            Id = item.MemberId,
                            Name = item.Member!.FullName,
                            Role = item.Role,
                            Selected = attendees.Contains(item.MemberId)
                        });
                    }
                    return invitees;
                default:
                    return new List<SelectableUserModel>();
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