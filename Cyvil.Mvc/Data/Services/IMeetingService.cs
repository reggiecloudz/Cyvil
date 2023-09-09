using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Data.Services
{
    public interface IMeetingService
    {
        // Add specific methods for the Category repository here
        Task<List<SelectableUserModel>> GetInvitees(int eventType, long id);
    }

}