using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ParkyAPI.Modals.Dtos
{
    public class NationalParkDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Creatred { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Established { get; set; }
    }
}
