using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class IndexVM
    {
        public IEnumerable<NationalParks> NationalParkList { get; set; }
        public IEnumerable<Trails> TrailsList { get; set; }
    }
}
