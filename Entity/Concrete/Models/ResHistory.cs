﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class ResHistory:IEntity
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
        public DateTime date { get; set; }
    }
}
