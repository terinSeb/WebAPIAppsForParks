﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Modals
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public double Elevation { get; set; }
        public enum DifficultyType { Easy,Moderate,Difficult,Expert}
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; }

        [ForeignKey("NationalParkId")]
        public NationalParks NationalPark { get; set; }

        public DateTime DateCreated { get; set; }
    }
}