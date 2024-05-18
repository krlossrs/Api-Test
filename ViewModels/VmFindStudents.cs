using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Api_Test
{
    public class VmFindStudents
    {
        [DefaultValue('F')]
        public char? Gender { get; set; }
        
        public bool isGenderAnd { get; set; }

        [DefaultValue(0)]
        public int? Age { get; set; }

        public bool isAgeAnd {  get; set; }

        [DefaultValue("")]
        public string? Education { get; set; }

        public bool isEducationAnd { get; set; }

        [DefaultValue(0)]
        public int? AcademicYear {  get; set; }

        public bool isAcademicYearAnd { get; set; }

    }
}

