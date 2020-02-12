using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Repositories;
using PhotoSN.WebMvcIdentity.Models;
using System;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Controllers
{
    public class SearchController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly IMapper _mapper;

        public SearchController(
            UserManager<User> userManager,
            IPhotoSNRepository photoSNRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _photoSNRepository = photoSNRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult SearchUsersByNickname(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                return View(new SearchModel { Query = query });
            }
            return View(new SearchModel());
        }

        [HttpGet]
        public IActionResult SearchPostsByHashtags(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                return View(new SearchModel { Query = query });
            }
            return View(new SearchModel());
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(SearchModel searchModel, int? userId = int.MaxValue)
        {
            try
            {
                if (searchModel == null ||
                    string.IsNullOrWhiteSpace(searchModel.Query))
                {
                    return Ok();
                }
                var users = await _photoSNRepository.GetUsersByNicknameAsync(searchModel.Query, userId.Value);
                return Json(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(SearchModel searchModel, int? postId = int.MaxValue)
        {
            try
            {
                if (searchModel == null ||
                    string.IsNullOrWhiteSpace(searchModel.Query))
                {
                    return Ok();
                }

                bool isAuthorized;
                int currentUserId;

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    isAuthorized = false;
                    currentUserId = 0;
                }
                else
                {
                    isAuthorized = true;
                    currentUserId = user.Id;
                }

                var posts = await _photoSNRepository.GetPostsByHashtagsAsync(searchModel.Query, isAuthorized, currentUserId, postId.Value);
                return Json(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}