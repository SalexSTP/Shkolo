using Shkolo.Enums;

namespace Shkolo.Models
{
    public class Remark
    {
        public Subject Subject { get; set; }
        public RemarkType RemarkType { get; set; }
        public DateTime Date { get; set; }

        public Remark(Subject subject, RemarkType remarkType)
        {
            Subject = subject;
            RemarkType = remarkType;
            Date = DateTime.Now;
        }
    }
}
