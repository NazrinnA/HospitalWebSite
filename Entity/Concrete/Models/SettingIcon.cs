
using Core.Entities;

public class SettingIcon:IEntity
    {
        public int? Id { get; set; }
        public string? IconTag { get; set; }
        public string? IconUrl { get; set; }
        public Setting? Setting { get; set; }
        public int? SettingId { get; set; }
    }

