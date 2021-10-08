using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace izo.Models
{
    public class ExamResult
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [Display(Name = "Student")]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        [Required]
        [Display(Name = "Course")]
        public Guid CourseID { get; set; }
        public Course Course { get; set; }
        [Range(1, 10)]
        public int Score { get; set; }
    }
}
