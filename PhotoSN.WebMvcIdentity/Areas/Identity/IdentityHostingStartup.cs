﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PhotoSN.WebMvcIdentity.Areas.Identity.IdentityHostingStartup))]
namespace PhotoSN.WebMvcIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}