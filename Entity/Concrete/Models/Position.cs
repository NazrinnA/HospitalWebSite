
using Core.Entities;

public class Position:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Doctor>? Doctors { get; set; }
    }

