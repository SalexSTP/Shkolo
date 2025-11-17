namespace Shkolo.Models
{
    public class School
    {
        public string Name { get; set; }    
        public List<Class> Classes { get; set; }
        public List<Teacher> Teachers { get; set; } 

        public School(string name)
        {
            Name = name;
            Classes = new List<Class>();
            Teachers = new List<Teacher>();
        }

        public void AddClass(Class newClass)
        {
            Classes.Add(newClass);
        }

        public void AddTeacher(Teacher newTeacher)
        {
            Teachers.Add(newTeacher);
        }
    }
}
