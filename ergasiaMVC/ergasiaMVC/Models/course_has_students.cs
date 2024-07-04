using ergasiaMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ergasiaMVC.Models
{
    public class course_has_students
    {
        [Required]  
        public int COURSE_idCOURSE { get; set; }

        [Required]  
        public int STUDENTS_RegistrationNumber { get; set; }

        public int GradeCourseStudent { get; set; }

        [ForeignKey("COURSE_idCOURSE")]
        public Course course { get; set; }

        [ForeignKey("STUDENTS_RegistrationNumber")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Student student { get; set; }    
    }

}

