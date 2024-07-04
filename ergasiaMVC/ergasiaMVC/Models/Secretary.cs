using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace ergasiaMVC.Models
{
    public class Secretary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Phonenumber { get; set; }

        [Required]
        [StringLength(45)]
        public string USERS_username { get; set; }    

        [Required]
        [StringLength(45)]
        public string Name { get; set; }    

        [Required]
        [StringLength(45)]
        public string Surname { get; set; } 

        [Required]
        [StringLength(45)]
        public string Department { get; set; }

        [ForeignKey("USERS_username")]
        public User user { get; set; }
    }
}
