using Cyvil.Mvc.Data;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class SelectableVolunteerViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public SelectableVolunteerViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            var team = await _context.Teams
                .Include(a => a.Members)
                .ThenInclude(a => a.Member)
                .FirstOrDefaultAsync(x => x.Id == id);
            var volunteers = _context.ProjectParticipants.Where(v => v.ProjectId == team!.ProjectId).Include(v => v.Participant).ToList();
            var teamMembers = new HashSet<string>(team!.Members.Select(u => u.MemberId));
            var volunteerList = new List<SelectableVolunteer>();

            foreach (var item in volunteers)
            {
                if (!teamMembers.Contains(item.ParticipantId))
                {
                    volunteerList.Add(new SelectableVolunteer
                    {
                        Value = item.ParticipantId,
                        Text = item.Participant!.FullName,
                        UserPhoto = $"media/members/{item.Participant!.ProfileImage}"
                    });
                }
                
            }

            var model = new AddTeamMemberModel
            {
                Volunteers = volunteerList
            };
            
            return View(model);
        }
    }
}