using Shkolo.Enums;

namespace Shkolo.Models
{
    public class Grade
    {
        public Subject Subject { get; set; }
        public double Value { get; set; }
        public DateTime DateReceived { get; set; }
        public GradeReason Reason { get; set; }
        public Teacher Teacher { get; set; }

        public Grade(Subject subject, double value, GradeReason reason, Teacher teacher)
        {
            Subject = subject;
            Value = value;
            DateReceived = DateTime.Now;
            Reason = reason;
            Teacher = teacher;
        }

        public override string ToString()
        {
            return $"{Subject}: {Value} ({Reason}) by {Teacher.FirstName} {Teacher.LastName} on {DateReceived.ToShortDateString()}";
        }
    }
}
