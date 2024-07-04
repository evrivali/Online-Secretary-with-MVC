using Microsoft.Extensions.Primitives;

namespace ergasiaMVC.Models
{
    public class AddCourseViewModel
    {
        public Course course { get; set; }
        public string secUsername { get; set; }
        public string message { get; set; }
        public string Department { get; set; }
        public int afm { get; set; }
    }
}
