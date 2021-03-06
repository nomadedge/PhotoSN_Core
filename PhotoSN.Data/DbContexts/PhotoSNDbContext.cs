﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoSN.Data.Entities;

namespace PhotoSN.Data.DbContexts
{
    public class PhotoSNDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<BlacklistRow> Bans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<InPostHashtag> InPostHashtags { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<PostImage> PostImages { get; set; }

        public PhotoSNDbContext(DbContextOptions<PhotoSNDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Avatar>().HasKey(a => new
            {
                a.UserId,
                a.ImageId
            });

            builder.Entity<BlacklistRow>().HasKey(br => new
            {
                br.FirstUserId,
                br.SecondUserId
            });
            builder.Entity<BlacklistRow>()
                .HasOne(br => br.FirstUser)
                .WithMany(u => u.Blacklist)
                .HasForeignKey(br => br.FirstUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<BlacklistRow>()
                .HasOne(br => br.SecondUser)
                .WithMany(u => u.BlockedBy)
                .HasForeignKey(br => br.SecondUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CommentLike>().HasKey(cl => new
            {
                cl.UserId,
                cl.CommentId
            });
            builder.Entity<CommentLike>()
                .HasOne(cl => cl.User)
                .WithMany(u => u.CommentLikes)
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Hashtag>().HasIndex(h => new { h.Text });

            builder.Entity<Image>()
                .HasOne(i => i.User)
                .WithMany(u => u.Images)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<InPostHashtag>().HasKey(iph => new
            {
                iph.HashtagId,
                iph.PostId
            });

            builder.Entity<PostLike>().HasKey(pl => new
            {
                pl.UserId,
                pl.PostId
            });
            builder.Entity<PostLike>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Subscription>().HasKey(s => new
            {
                s.FirstUserId,
                s.SecondUserId
            });
            builder.Entity<Subscription>()
                .HasOne(s => s.FirstUser)
                .WithMany(u => u.Following)
                .HasForeignKey(s => s.FirstUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Subscription>()
                .HasOne(s => s.SecondUser)
                .WithMany(u => u.Followers)
                .HasForeignKey(s => s.SecondUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
