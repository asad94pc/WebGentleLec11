using Lec11.Data;
using Lec11.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookDbContext _dbContext = null;

        public LanguageRepository(BookDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<LanguageModel>> GetAllLanguages()
        {
            return await _dbContext.Languages.Select(x => new LanguageModel()
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description,
            }).ToListAsync();
        }
    }
}
