using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Readings_Guide.Cores.AppDbContext;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Readings_Guide.Cores.Services
{
    public class AuthService : IAuth
    {
        private readonly UserManager<ApplicationUser> usermanager;

        public AuthService(UserManager<ApplicationUser> usermanager)
        {
            this.usermanager = usermanager;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModelDto model)
        {
            if (await usermanager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email Is Exist" };
            }

            if (await usermanager.FindByNameAsync(model.UserName) is not null)
            {
                return new AuthModel { Message = "UserName Is Exist" };
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await usermanager.CreateAsync(user);
            if (!result.Succeeded)
                return new AuthModel { Message = "SomeThing Wronge" };

            var token = await CreateToken(user);
            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                IsAuthntecation = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = token.ValidTo
            };
        }

        public async Task<AuthModel> LoginAsync(LoginModelDto model)
        {
            var user = await usermanager.FindByEmailAsync(model.Email);
            if(user == null || !await usermanager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthModel { Message = "Email Or Password Is Incorrect" };
            }

            var token = await CreateToken(user);
            return new AuthModel
            {
                Email = user.Email,
                UserName = user.UserName,
                IsAuthntecation = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = token.ValidTo
            };
        }

        public async Task<bool> ChangePasswordAsync(string userid, ChangePasswordDto model)
        {
            var user = await usermanager.FindByIdAsync(userid);
            if(user == null) return false;

            var checkpassword = await usermanager.CheckPasswordAsync(user, model.OldPassword);
            if(!checkpassword) return false;

            var result = await usermanager.ChangePasswordAsync(user,model.OldPassword,model.NewPassword);
            return result.Succeeded;
        }

        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var roles = await usermanager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securitykey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes("JHGKJHFHGJDRSTRD&^RFHGFJVGLJHFDDKKVccxzsggsgdsgdwuygeygdhgswbndkvsncvbdvsjhvsjv27961hcghcvznmvchdsvcnzvbJLHJKKLJVKchsvgvH"));

            var signincred = new SigningCredentials
                (securitykey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
                (
                  issuer: "https://localhost:44394",
                  audience: "https://localhost:4200",
                  expires: DateTime.Now.AddHours(1),
                  claims: claims,
                  signingCredentials: signincred
                );
            return jwt;
        }
    }
}

