using System.ComponentModel.DataAnnotations;

namespace Shkolo.Enums
{
    public enum GradeReason
    {
        [Display(Name = "Exam")]
        Exam,

        [Display(Name = "Active Participation")]
        ActiveParticipation,

        [Display(Name = "Homework")]
        Homework,

        [Display(Name = "Project")]
        Project,

        [Display(Name = "Oral Examination")]
        OralExamination,

        [Display(Name = "Entrance Exam")]
        EntranceExam,

        [Display(Name = "Term Exam")]
        TermExam,

        [Display(Name = "Final Exam")]
        FinalExam,

        [Display(Name = "Other")]
        Other
    }
}
