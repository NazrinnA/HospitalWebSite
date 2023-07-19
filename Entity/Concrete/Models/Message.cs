
using Core.Entities;

public class Message:IEntity
    {
        public int Id { get; set; }
        public string Subject { get; set; } 
        public string Letter { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }   
        public bool IsDeleted { get; set; } 
        public bool? IsActive { get; set; }  
    }
    

