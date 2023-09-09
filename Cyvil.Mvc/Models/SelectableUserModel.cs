namespace Cyvil.Mvc.Models
{
    public class SelectableUserModel
    {
        public SelectableUserModel(){}

        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool Selected { get; set; } = false;
    }
}