﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecars.Model
{
    public class BaseDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } // Name of the car
        [Required]
        public int ModelYear { get; set; } // Year the car was manufactured
        [Required]
        public double Price { get; set; } // Price of the car
        [Required]
        public List<string> Colors { get; set; } // Colors of the car
        public double Mileage { get; set; } // Mileage of the car
    }
}
