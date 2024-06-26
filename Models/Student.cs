using System.ComponentModel.DataAnnotations;

namespace Api_Test
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[FM]$", ErrorMessage = "Allowed values F or M")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(17, 36, ErrorMessage = "Age between 17 to 36 years")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Required")]
        public string? Education { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(1, 6, ErrorMessage = "Academic Year between 1 to 6 years")]
        public int AcademicYear {  get; set; }

        public Student() { }

        public Student(VmAddStudent vmStudent)
        {
            Name = vmStudent.Name;
            Gender = vmStudent.Gender;
            Education = vmStudent.Education;
            AcademicYear = vmStudent.AcademicYear;
            Age = vmStudent.Age;
        }

    }
}
