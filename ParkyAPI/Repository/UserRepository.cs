using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;
using ParkyAPI.Repository.IRepository;
using ParkyAPI.Data;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ParkyAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDBContext _db;
        public readonly AppSettings _appSettings;
        public UserRepository(ApplicationDBContext db, IOptions<AppSettings> appsetings)
        {
            _db = db;
            _appSettings = appsetings.Value;
        }
        public Users Authenticate(string Username, string Password)
        {
            var user = _db.users.SingleOrDefault(x => x.UserName == Username && x.Password == Password);
            if(user == null)
            {
                return null;
            }
            //If User was Found Generate JWT Tocken
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }

        public bool IsUniqueUser(string UserName)
        {
            throw new NotImplementedException();
        }

        public Users Register(string Username, string Password)
        {
            throw new NotImplementedException();
        }
    }
}
