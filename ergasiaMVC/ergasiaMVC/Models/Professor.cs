using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ergasiaMVC.Models
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AFM { get; set; }

        [Required]
        [StringLength(45)]
        public string USERS_username { get; set; }

        [Required]
        [StringLength(45)]
        public string Surname { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        public string Department { get; set; }

        [ForeignKey("USERS_username")]
        public User user { get; set; }
    }
}
