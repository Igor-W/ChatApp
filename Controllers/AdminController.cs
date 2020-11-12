using AutoMapper;
using ChatApp.DTOs;
using ChatApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/admin-panel")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AdminController(UserManager<User> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        //GET /api/admin-panel/number-of-users
        [HttpGet]
        [Route("number-of-users")]
        public async Task<ActionResult<int>> GetNumberOfUsers()
        {
            return await _userManager.Users.CountAsync();
        }

        //GET /api/admin-panel/all-users
        [HttpGet]
        [Route("all-users")]
        public async Task<ActionResult<IEnumerable<UserReadByAdminDTO>>> GetAllUsers()
        {
            var users = await _userManager.Users.OrderBy(u => u.LastName).ToListAsync();

            return users.Any() ? Ok(_mapper.Map<IEnumerable<UserReadByAdminDTO>>(users)) : (ActionResult)NotFound();
        }

        //GET /api/admin-panel/get-by-id/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        [Route("get-by-id/{id}")]
        public async Task<ActionResult<UserReadByAdminDTO>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user != null ? Ok(_mapper.Map<UserReadByAdminDTO>(user)) : (ActionResult)NotFound();
        }

        //POST /api/admin-panel/create-user
        [HttpPost]
        [Route("create-user")]
        public async Task<ActionResult<UserCreateByAdminDTO>> CreateUser(UserCreateByAdminDTO userToCreate)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            var user = _mapper.Map<User>(userToCreate);

            if (user.Gender == true)
                user.ProfileImage = _configuration.GetSection("AvatarString").GetSection("FemaleAvatar").Value;
            else
                user.ProfileImage = _configuration.GetSection("AvatarString").GetSection("MaleAvatar").Value;

            await _userManager.CreateAsync(user, userToCreate.Password);

            if (user.Position == "Administrator")
                await _userManager.AddToRoleAsync(user, "Admin");
            else
                await _userManager.AddToRoleAsync(user, "User");

            return CreatedAtRoute(nameof(GetUserById), new { user.Id }, user);
        }

        //PUT /api/admin-panel/update-user/{id}
        [HttpPut("{id}")]
        [Route("update-user/{id}")]
        public async Task<ActionResult> UpdateUser(string id, UserUpdateByAdminDTO userUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            var userToBeUpdated = await _userManager.FindByIdAsync(id);

            if (userToBeUpdated == null)
                return NotFound();

            _mapper.Map(userUpdateDto, userToBeUpdated);
            await _userManager.UpdateAsync(userToBeUpdated);

            if (userToBeUpdated.Position == "Administrator")
            {
                await _userManager.AddToRoleAsync(userToBeUpdated, "Admin");
                await _userManager.RemoveFromRoleAsync(userToBeUpdated, "User");
            }
            else
            {
                await _userManager.AddToRoleAsync(userToBeUpdated, "User");
                await _userManager.RemoveFromRoleAsync(userToBeUpdated, "Admin");
            }

            return NoContent();
        }

        //DELETE /api/admin-panel/delete-user/{id}
        [HttpDelete("{id}")]
        [Route("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var userToBeDeleted = await _userManager.FindByIdAsync(id);

            if (userToBeDeleted == null)
                return NotFound();

            await _userManager.DeleteAsync(userToBeDeleted);

            return NoContent();
        }
    }
}

