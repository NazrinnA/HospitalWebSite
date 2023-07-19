﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Models
{
    public class Holiday:IEntity
    {
        public int Id { get; set; } 
        public bool Permission { get; set; }    
    }
}
