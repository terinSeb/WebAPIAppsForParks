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
    [Route("api/v{version:apiVersion}/Users")]
    //[Route("api/[controller]")]
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
        public IActionResult Authenticate([FromBody] AuthenticateModal model)
        {
            var user = _userrepo.Authenticate(model.Username, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Usernameor Password is Incoorect." });
            }
            return Ok(user);
        }
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthenticateModal modal )
        {
            bool IfUserNameUnique = _userrepo.IsUniqueUser(modal.UserName);
            if(!IfUserNameUnique)
            {
                return BadRequest(new { message = "Username already exists" });
            }
            var user = _userrepo.Register(modal.UserName, modal.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Error while Registring" });
            }
            return Ok();
        }
    }
}
