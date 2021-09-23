using DeviceMonnitorAPI.DBModels;
using DeviceMonnitorAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Services
{
    public class UserService
    {
        private MyDBContext _myDbContext;
        private readonly ApplicationSettings _appSettings;

        public UserService(MyDBContext context, IOptions<ApplicationSettings> appSettings)
        {
            _myDbContext = context;
            _appSettings = appSettings.Value;
        }

        public async Task<UserModel> Authorize(UserModel model)
        {
            try
            {                
                var user = _myDbContext.Users.Where(c => c.Username == model.username && c.Password == model.password && (c.IsActive || !c.Role.Equals("apiadmin"))).FirstOrDefault();
                if (user != null && user != null)
                {
                    IdentityOptions _options = new IdentityOptions();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserName",user.Username.ToString()),
                        new Claim("UserId",user.UserGuid.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,user.Role)
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JWT_SecureKey)), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    model.password = "";
                    model.expires = tokenDescriptor.Expires;
                    model.token = token;

                    return model;
                }                
            }
            catch (Exception e)
            {
                throw;
            }
            return null;

        }

        public async Task<Guid?> EditUser(OrgUserModel model)
        {
            try
            {
                User user = _myDbContext.Users.SingleOrDefault(c=>c.UserGuid == model.UserGuid);

                //user.Id = model.Id;
                user.UserGuid = model.UserGuid;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Role = model.Role;
                user.IsActive = model.IsActive;                
                user.EditedDate = DateTime.Now;
                //var result = _myDbContext.(user);
                await _myDbContext.SaveChangesAsync();
                return user.UserGuid;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Guid?> AddUser(OrgUserModel model)
        {
            try
            {
                User user = new User();
                
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Role = model.Role;
                user.IsActive = model.IsActive;                

                user.UserGuid = Guid.NewGuid();
                user.CreatedDate = DateTime.Now;

                var result = _myDbContext.AddAsync(user);
                await _myDbContext.SaveChangesAsync();
                return user.UserGuid;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public async Task<Guid?> DeleteUser(Guid guid)
        {
            try
            {
                var result = await this.GetUserByGuid(guid);
                _myDbContext.Users.Remove(result);
                await _myDbContext.SaveChangesAsync();
                return result.UserGuid;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> GetUserByGuid(Guid guid)
        {
            try
            {
                var result = _myDbContext.Users.Where(c => c.UserGuid == guid).FirstOrDefault();
                result.Password = "";
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<OrgUserModel>> GetUsers()
        {
            try
            {
                //_myDbContext.ChangeTracker.LazyLoadingEnabled = false;
                var result = _myDbContext.Users.ToList().OrderBy(c=>c.CreatedDate);//.Select(c => new { c.Id, c.UserGuid, c.FirstName, c.LastName, c.Username, c.Password, c.Role, c.IsActive, c.Description, c.CreatedDate, c.EditedDate});                
                var listResult = new List<OrgUserModel>();

                foreach (var res in result) {
                    listResult.Add(new OrgUserModel
                    {
                        UserGuid = res.UserGuid,
                        FirstName = res.FirstName,
                        LastName = res.LastName,
                        Username = res.Username,
                        Password = res.Password,
                        Role = res.Role,
                        IsActive = res.IsActive,
                        Description = res.Description,
                        CreatedDate = res.CreatedDate,
                        EditedDate = res.EditedDate
                    });
                }

                return listResult;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
