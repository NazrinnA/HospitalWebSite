﻿
using Core.Entities;

public class Icon:IEntity
    {
        public int? Id { get; set; }
        public string? IconTag { get; set; }
        public string? IconUrl { get; set; }
        public Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
    }

