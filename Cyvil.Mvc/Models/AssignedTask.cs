namespace Cyvil.Mvc.Models
{
    public class AssignedTask
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Assigned { get; set; }
    }
}