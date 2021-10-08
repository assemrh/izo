using izo.Data;
using izo.Services;
using izo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly DataDbContext _context;

        public LanguageService(DataDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _context.LanguagesList.ToList();
        }

        public Language GetLanguageByCulture(string culture)
        {
            return _context.LanguagesList.FirstOrDefault(x =>
                x.Culture.Trim().ToLower() == culture.Trim().ToLower());
        }
    }
}
