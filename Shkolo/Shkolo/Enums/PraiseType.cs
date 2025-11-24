using System.ComponentModel.DataAnnotations;

namespace Shkolo.Enums
{
    public enum PraiseType
    {
        [Display(Name = "Active Participation")]
        ActiveParticipation,

        [Display(Name = "Excellent Performance")]
        ExcellentPerformance,

        [Display(Name = "Improvement")]
        Improvement,

        [Display(Name = "Teamwork")]
        Teamwork,

        [Display(Name = "Leadership")]
        Leadership,

        [Display(Name = "Creativity")]
        Creativity,

        [Display(Name = "Consistency")]
        Consistency,

        [Display(Name = "Responsibility")]
        Responsibility,

        [Display(Name = "Task Completion")]
        TaskCompletion,

        [Display(Name = "Nap")]
        Nap,

        [Display(Name = "Other")]
        Other
    }
}
