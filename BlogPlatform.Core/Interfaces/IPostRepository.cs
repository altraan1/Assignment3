using BlogPlatform.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);

        // Crud: create, read, update, delete
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task UpdatePartialAsync(Post post);
        Task DeleteAsync(int id);

        // these are utility methods
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}