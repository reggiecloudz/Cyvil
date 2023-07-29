using System.ComponentModel.DataAnnotations;

namespace Cyvil.Mvc.Domain
{
    public abstract class Entity : IEntity
    {
        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Updated { get; set; }
    }
}
