namespace Shkolo.Models
{
    public class Teacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Subject}";
        }
    }
}
