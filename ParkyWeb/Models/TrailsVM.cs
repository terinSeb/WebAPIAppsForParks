﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class TrailsVM
    {
        public IEnumerable<SelectListItem> NationalParkList { get; set; }
        public Trails Trail { get; set; }
    }
}
