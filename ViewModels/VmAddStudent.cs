using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Api_Test
{
    public class VmAddStudent
    {

        [Required(ErrorMessage = "Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[FM]$", ErrorMessage = "Allowed values F or M")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(17, 36, ErrorMessage = "Age between 17 to 36 years")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Required")]
        public  string Education { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(1, 6, ErrorMessage = "Academic Year between 1 to 6 years")]
        public int AcademicYear {  get; set; }

    }
}

