using Microsoft.Extensions.Primitives;

namespace ergasiaMVC.Models
{
    public class AddProfessorViewModel
    {
        public string password { get; set; }
        public Professor professor { get; set; }
        public string secUsername { get; set; }
        public string message { get; set; }
    }
}
