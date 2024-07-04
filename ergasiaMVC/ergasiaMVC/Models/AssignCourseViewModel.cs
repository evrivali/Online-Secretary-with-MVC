using Microsoft.Extensions.Primitives;

namespace ergasiaMVC.Models
{
    public class AssignCourseViewModel
    {
        public int course { get; set; }
        public int student { get; set; }
        public string secUsername { get; set; }
        public string message { get; set; }
        public string messageS { get; set; }
        public string Department { get; set; }
    }
}
