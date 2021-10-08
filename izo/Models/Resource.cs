using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Models
{
    public class Resource
    {
        [Key]
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }//
        public string KeyName { get; set; }
        public string Value { get; set; }

        public Language Language { get; set; }
    }
}
