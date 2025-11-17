using Shkolo.Enums;

namespace Shkolo.Models
{
    public class Praise
    {
        public Subject Subject { get; set; }
        public PraiseType PraiseType { get; set; }
        public DateTime Date { get; set; }

        public Praise(Subject subject, PraiseType praiseType)
        {
            Subject = subject;
            PraiseType = praiseType;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{PraiseType} in {Subject} on {Date.ToShortDateString()}";
        }
    }
}
