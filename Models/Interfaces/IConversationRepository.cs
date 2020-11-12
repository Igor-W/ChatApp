using ChatApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Models.Interfaces
{
    public interface IConversationRepository
    {
        Task SaveChangesAsync();
        Task<IEnumerable<Conversation>> GetAllConversationsAsync();
        Task<Conversation> GetConversationByIdAsync(int id);
        Task<IEnumerable<Conversation>> GetconversationBetweenTwoUsersAsync(string senderId, string recieverId);
        Task CreateConversationAsync(Conversation conversation);
        Task DeleteConversationAsync(Conversation conversation);
        Task DeleteAllConversationsAsync();
    }
}
