using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoSN.Data.DbContexts;
using PhotoSN.Data.Entities;

[assembly: HostingStartup(typeof(PhotoSN.WebMvcIdentity.Areas.Identity.IdentityHostingStartup))]
namespace PhotoSN.WebMvcIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}