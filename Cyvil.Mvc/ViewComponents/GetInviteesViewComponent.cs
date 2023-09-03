using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyvil.Mvc.ViewComponents
{
    public class GetInviteesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public GetInviteesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventType, long id)
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
                    return View(participants);
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
                    return View(applicants);
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
                    return View(members);
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
                    return View(invitees);
                default:
                    return View(new List<InviteeListModel>());
            }
        }
    }
}