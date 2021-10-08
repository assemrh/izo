using izo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Services
{
    public interface ILocalizationService
    {
        Resource GetStringResource(string resourceKey, int languageId);
    }
}
