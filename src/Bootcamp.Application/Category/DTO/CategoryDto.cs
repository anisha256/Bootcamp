﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.Category.DTO
{
    public  class CategoryDto
    {
       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool DeleteFlag { get; set;}
         
       
    }
}
