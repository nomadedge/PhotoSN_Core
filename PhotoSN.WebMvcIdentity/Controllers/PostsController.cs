using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoSN.Data.Dtos;
using PhotoSN.Data.Entities;
using PhotoSN.Data.Repositories;
using PhotoSN.WebMvcIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Controllers
{
    public class PostsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IPhotoSNRepository _photoSNRepository;
        private readonly IMapper _mapper;

        public PostsController(
            UserManager<User> userManager,
            IPhotoSNRepository photoSNRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _photoSNRepository = photoSNRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePost()
        {
            var model = new PostModel { ImageIds = new List<int>() };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostModel postModel)
        {
            try
            {
                if (postModel == null ||
                    postModel.ImageIds == null ||
                    !postModel.ImageIds.Any() ||
                    postModel.ImageIds.Count > 10)
                {
                    throw new ArgumentOutOfRangeException("Post must contain from 1 to 10 pictures.");
                }

                var userId = (await _userManager.GetUserAsync(User)).Id;
                var createPostDto = _mapper.Map<CreatePostDto>(postModel);
                createPostDto.UserId = userId;
                var postId = await _photoSNRepository.CreatePostAsync(createPostDto);

                return StatusCode(StatusCodes.Status201Created, postId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFullPost(int id)
        {
            try
            {
                var getFullPostDto = await _photoSNRepository.GetFullPostAsync(id);
                return View(getFullPostDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeOrDislikePost(int id)
        {
            try
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var isLikedNow = await _photoSNRepository.LikeOrDislikePostAsync(userId, id);
                return Ok(isLikedNow);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPostsByUserId(int id, int? postId = int.MaxValue)
        {
            try
            {
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

                var posts = await _photoSNRepository.GetPostsByUserIdAsync(id, isAuthorized, currentUserId, postId.Value);
                return Json(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFeed(int? postId = int.MaxValue)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var posts = await _photoSNRepository.GetFeedByUserIdAsync(user.Id, postId.Value);

                return Json(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId(int id, int? commentId = int.MaxValue)
        {
            try
            {
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

                var comments = await _photoSNRepository.GetCommentsByPostIdAsync(id, isAuthorized, currentUserId, commentId.Value);
                return Json(comments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeOrDislikeComment(int id)
        {
            try
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var isLikedNow = await _photoSNRepository.LikeOrDislikeCommentAsync(userId, id);
                return Ok(isLikedNow);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CommentModel commentModel)
        {
            try
            {
                if (commentModel == null ||
                    string.IsNullOrWhiteSpace(commentModel.Text))
                {
                    throw new ArgumentException("Comment can't be empty or consists only a white-space characters.");
                }

                var userId = (await _userManager.GetUserAsync(User)).Id;
                var createCommentDto = _mapper.Map<CreateCommentDto>(commentModel);
                createCommentDto.UserId = userId;

                var newComment = await _photoSNRepository.CreateCommentAsync(createCommentDto);
                return Json(newComment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}