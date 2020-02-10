using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Repositories;
using PhotoSN.WebMvcIdentity.Models;
using System;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly IMapper _mapper;

        public UsersController(
            UserManager<User> userManager,
            IPhotoSNRepository photoSNRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _photoSNRepository = photoSNRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var getUserDto = await _photoSNRepository.GetUserAsync(id);
                var userModel = _mapper.Map<UserModel>(getUserDto);
                return View(userModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FollowToUser(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                await _photoSNRepository.FollowToUserAsync(user.Id, id);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> UnfollowFromUser(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                await _photoSNRepository.UnfollowFromUserAsync(user.Id, id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFollowers(int id)
        {
            try
            {
                var followers = await _photoSNRepository.GetFollowersAsync(id);

                return Json(followers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFollowings(int id)
        {
            try
            {
                var followings = await _photoSNRepository.GetFollowingsAsync(id);

                return Json(followings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}