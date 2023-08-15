using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum MessageThreadType
    {
        [Description("Project")]
        Project,
        [Description("Action Item")]
        ActionItem,
        [Description("Position")]
        Position,
        [Description("General")]
        General
    }
}