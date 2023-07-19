
using Core.Entities;

public class Rezerv:IEntity
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public DateTime date { get; set; }
        public bool Busy { get; set; }
        public string? UserEmail { get; set; }
        public Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
    }
    
