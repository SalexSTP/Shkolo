using System.ComponentModel.DataAnnotations;

namespace Shkolo.Enums
{
    public enum RemarkType
    {
        [Display(Name = "No Homework")]
        NoHomework,

        [Display(Name = "Disruptive Behavior")]
        DisruptiveBehavior,

        [Display(Name = "Late Submission")]
        LateSubmission,

        [Display(Name = "Concentration Issues")]
        ConcentrationIssues,

        [Display(Name = "Bad Discipline")]
        BadDiscipline,

        [Display(Name = "Sleeping In Class")]
        SleepingInClass,

        [Display(Name = "Eating In Class")]
        EatingInClass,

        [Display(Name = "Unprepared For Class")]
        UnpreparedForClass,

        [Display(Name = "Rude Behavior")]
        RudeBehavior,

        [Display(Name = "Other")]
        Other
    }
}
