﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _npRepo;
        private readonly ITrailRepository _trailReop;
        public HomeController(ILogger<HomeController> logger,
            INationalParkRepository npRepo,
            ITrailRepository trailRepo)
        {
            _logger = logger;
            _npRepo = npRepo;
            _trailReop = trailRepo;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM listOfParksandTrails = new IndexVM()
            {
                NationalParkList = await _npRepo.GetAllAsync(SD.NaptionalParkAPIPath),
                TrailsList = await _trailReop.GetAllAsync(SD.TrailParkAPIPath)
            };
            return View(listOfParksandTrails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
