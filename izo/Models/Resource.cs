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
        public int Id { get; set; }
        public int LanguageId { get; set; }//LanguageId
        public string KeyName { get; set; }
        public string Value { get; set; }

        public Language Language { get; set; }
    }
}
