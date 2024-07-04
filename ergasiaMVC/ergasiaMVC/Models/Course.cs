using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ergasiaMVC.Models
{
    public class Course
    {
        [Key]
        public int idCOURSE { get; set; }

        [Required]
        [StringLength(45)]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(25)]
        public string CourseSemester { get; set; }
            
        public int? PROFESSORS_AFM { get; set; }

        [ForeignKey("PROFESSORS_AFM")]
        public Professor professor { get; set; }
    }
}
