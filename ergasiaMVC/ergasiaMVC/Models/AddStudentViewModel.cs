using Microsoft.Extensions.Primitives;

namespace ergasiaMVC.Models
{
    public class AddStudentViewModel
    {
        public string password { get; set; }
        public Student student { get; set; }
        public string secUsername { get; set; }
        public string message { get; set; }
    }
}
