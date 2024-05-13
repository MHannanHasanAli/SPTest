using System.ComponentModel.DataAnnotations;

namespace SPTest
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        [StringLength(50)]
        public string Designation { get; set; } = string.Empty;
    }
}
