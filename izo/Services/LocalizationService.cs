using izo.Data;
using izo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly DataDbContext _context;

        public LocalizationService(DataDbContext context)
        {
            _context = context;
        }

        public Resource GetStringResource(string resourceKey, Guid languageId)
        {
            return _context.Resources.FirstOrDefault(x =>
                    x.KeyName.Trim().ToLower() == resourceKey.Trim().ToLower()
                    && x.LanguageId == languageId);
        }

        Resource ILocalizationService.GetStringResource(string resourceKey, Guid languageId)
        {
            throw new NotImplementedException();
        }
    }
}
