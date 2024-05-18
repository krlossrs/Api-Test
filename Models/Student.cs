namespace Api_Test
{
    public class Student
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public char Gender { get; set; }

        public int Age { get; set; }

        public  string? Education { get; set; }

        public int AcademicYear {  get; set; }

        public Student() { }

        public Student(VmStudent vmStudent)
        {
            Name = vmStudent.Name;
            Gender = vmStudent.Gender;
            Education = vmStudent.Education;
            AcademicYear = vmStudent.AcademicYear;
            Age = vmStudent.Age;
        }

    }
}
