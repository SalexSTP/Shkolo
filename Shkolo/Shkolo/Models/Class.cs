namespace Shkolo.Models
{
    public class Class
    {
        public Teacher MainTeacher { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public Class(Teacher mainTeacher, string name)
        {
            MainTeacher = mainTeacher;
            Name = name;
            Students = new List<Student>();
        }

        public override string ToString()
        {
            return $"Class: {Name}, Main Teacher: {MainTeacher.FirstName} {MainTeacher.LastName}, Students Count: {Students.Count}";
        }

        public double CalculateClassAverageGrade()
        {
            if (Students.Count == 0)
                return 0.0;
            double total = 0.0;
            foreach (var student in Students)
            {
                total += student.CalculateAverageGrade();
            }
            return total / Students.Count;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public Student GetStudent(string name)
        {
            string firstName = name.Split(' ')[0];  
            string lastName = name.Split(' ')[1];
            return Students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}
