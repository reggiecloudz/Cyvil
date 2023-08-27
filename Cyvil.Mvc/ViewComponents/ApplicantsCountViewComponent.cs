using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class ApplicantsCountViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public ApplicantsCountViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long positionId)
        {
            var count = await _context.Applicants
                .Where(a => a.PositionId == positionId && a.ApplicantStatus == ApplicantStatus.Applied)
                .Select(a => a.Id)
                .LongCountAsync();

            return View(count);
        }
    }
}