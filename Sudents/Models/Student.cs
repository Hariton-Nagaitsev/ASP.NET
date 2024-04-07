using System.ComponentModel.DataAnnotations;

namespace Sudents.Models
{
    public class Student
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Surname { get; set; }

        [Required]
        public virtual string Subject { get; set; }

        [Required]
        public virtual int Score { get; set; }
    }
}
