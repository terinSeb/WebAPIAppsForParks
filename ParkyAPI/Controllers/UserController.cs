using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Modals;
using ParkyAPI.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userrepo;
        public UserController(IUserRepository userrepo)
        {
            _userrepo = userrepo;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] Users model)
        {
            var user = _userrepo.Authenticate(model.UserName, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Usernameor Password is Incoorect." });
            }
            return Ok(user);
        }
    
    }
}
