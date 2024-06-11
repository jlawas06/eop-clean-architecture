using System.ComponentModel;

namespace EOP.Domain.Enumerations
{
    public enum ElectionStatusEnum
    {
        [Description("Not Started")]
        NotStarted = 0,
        [Description("In Progress")]
        InProgress = 1,
        [Description("Finished")]
        Finished = 2,
        [Description("Cancelled")]
        Cancelled = 3
    }
}
