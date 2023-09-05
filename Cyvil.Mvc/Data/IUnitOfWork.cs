using Cyvil.Mvc.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}