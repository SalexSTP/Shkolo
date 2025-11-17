using System.Text;

namespace Shkolo.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Praise> Praises { get; set; }
        public List<Remark> Remarks { get; set; }
        public List<Grade> Grades { get; set; }

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Grades = new List<Grade>();
            Praises = new List<Praise>();
            Remarks = new List<Remark>();
        }

        public double CalculateAverageGrade()
        {
            if (Grades.Count == 0)
                return 0.0;
            double total = 0.0;
            foreach (var grade in Grades)
            {
                total += grade.Value;
            }
            return total / Grades.Count;
        }

        public void AddGrade(Grade grade)
        {
            Grades.Add(grade);
        }

        public void AddPraise(Praise praise)
        {
            Praises.Add(praise);
        }

        public void AddRemark(Remark remark)
        {
            Remarks.Add(remark);
        }

        public string GetFeedbackSummary()
        {
            return $"Student: {FirstName} {LastName}\n" +
                   $"Average Grade: {CalculateAverageGrade():F2}\n" +
                   $"Praises: {Praises.Count}\n" +
                   $"Remarks: {Remarks.Count}";
        }

        public string GetPraises()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var praise in Praises)
            {
                sb.AppendLine(praise.ToString());
            }

            return sb.ToString();
        }

        public string GetRemarks()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var remark in Remarks)
            {
                sb.AppendLine(remark.ToString());
            }

            return sb.ToString();
        }
    }
}
