﻿using Microsoft.EntityFrameworkCore;
using User_Management_API.DbContexts;
using User_Management_API.Entities;
using User_Management_API.Models;

namespace User_Management_API.Services;

public class PostRepository(UserManagementDbContext context) : IPostRepository
{
    private readonly UserManagementDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Reactions)
            .Include(p => p.Comments)
            .OrderBy(p => p.Date)
            .ToListAsync();
    }
    
    public async Task<Post?> GetPostByIdAsync(int postId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Reactions)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(u => u.Id == postId);
    }
    
    public async Task<Post> CreatePostAsync(PostForCreationDto postForCreationDto)
    {
        var newPost = new Post
        {
            Title = postForCreationDto.Title,
            Content = postForCreationDto.Content,
            Date = DateTime.Now,
        };

        _context.Posts.Add(newPost);
        await _context.SaveChangesAsync();

        return newPost;
    }

    public async Task<Post> UpdatePostAsync(int postId, PostForUpdateDto postForUpdateDto)
    {
        var existingPost = await _context.Posts.FindAsync(postId);
        if (existingPost == null)
        {
            throw new ArgumentException("Post not found");
        }

        existingPost.UpdatePost(postForUpdateDto.Title, postForUpdateDto.Content);
        await _context.SaveChangesAsync();

        return existingPost;
    }

    public async Task DeletePostAsync(int postId)
    {
        var post = await _context.Posts.FindAsync(postId);
        if (post == null) return;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}