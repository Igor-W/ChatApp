using AutoMapper;
using ChatApp.DTOs;
using ChatApp.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-panel")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;

            
        }

        [HttpGet]
        [Route("get-current-user")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            return user != null ? Ok(user) : (ActionResult)NotFound();
        }

        [HttpGet]
        [Route("get-token")]
        public async Task<ActionResult> GetToken()
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");

            return Ok(new { Token = token });
        }

        [HttpGet]
        [Route("all-users")]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsersWithoutCurrentAndAdmins()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currentUser = await _userManager.FindByEmailAsync(userEmail);

            var allUsers = await _userManager.Users.Where(u => u.Id != currentUser.Id).ToListAsync();
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var users = allUsers.Except(admins).ToList();

            return users.Any() ? Ok(_mapper.Map<IEnumerable<UserReadDTO>>(users)) : (ActionResult)NotFound();
        }

        [HttpPut("{id}")]
        [Route("update-user/{id}")]
        public async Task<ActionResult> UpdateCommand(string id, UserUpdateDTO userUpdateDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            var userToBeUpdated = await _userManager.FindByIdAsync(id);

            if (userToBeUpdated == null)
                return NotFound();

            _mapper.Map(userUpdateDto, userToBeUpdated);
            await _userManager.UpdateAsync(userToBeUpdated);

            return NoContent();
        }
    }
}
