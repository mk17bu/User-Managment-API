using User_Management_API.Entities;
using User_Management_API.Models;

namespace User_Management_API.Services;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPostsAsync();
    Task<Post?> GetPostByIdAsync(int postId);
    Task<Post> CreatePostAsync(PostForCreationDto postForCreationDto);
    Task<Post> UpdatePostAsync(int postId, PostForUpdateDto postForUpdateDto);
    Task DeletePostAsync(int postId);
}