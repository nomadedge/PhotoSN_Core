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

        [HttpPost]
        [Authorize]
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
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var getPostDto = await _photoSNRepository.GetPostAsync(id);
                return View(getPostDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}