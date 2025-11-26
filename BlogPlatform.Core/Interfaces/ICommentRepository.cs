using BlogPlatform.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPlatform.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);

        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}