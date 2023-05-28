using Lec11.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lec11.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetAllLanguages();
    }
}