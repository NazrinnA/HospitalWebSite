
using Core.Entities;

public class Service:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string? IconUrl { get; set; }
    }

