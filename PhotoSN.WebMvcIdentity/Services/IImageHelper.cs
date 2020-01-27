﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PhotoSN.WebMvcIdentity.Services
{
    public interface IImageHelper
    {
        Task<byte[]> ReadImageAsync(Guid guid);
        Task SaveImageAsync(IFormFile image, Guid guid);
        Task SaveAvatarAsync(IFormFile image, Guid guid);
        void DeleteImage(Guid guid);
    }
}