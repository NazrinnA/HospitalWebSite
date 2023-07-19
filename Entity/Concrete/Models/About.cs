
using Core.Entities;

public class About :IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Button>? Buttons { get; set; }
    }

