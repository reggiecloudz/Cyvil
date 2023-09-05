using Cyvil.Mvc.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        // Usage
        // private readonly IUnitOfWork _unitOfWork;

        //   public CategoriesController(
        //       ILogger<CategoriesController> logger,
        //       IUnitOfWork unitOfWork)
        //   {
        //       _logger = logger;
        //       _unitOfWork = unitOfWork;
        //   }

        //   [HttpGet]
        //   public async Task<IActionResult> Get()
        //   {
        //       var categories = await _unitOfWork.Categories.All();
        //       return Ok(categories);
        //   }


    }
}