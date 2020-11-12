using ChatApp.Context;
using ChatApp.Entities;
using ChatApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Repositories
{
    public class ConversationRepositorySQL : IConversationRepository
    {
        private AppDbContext _appDbContext;
        public ConversationRepositorySQL(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;

        }

        public async Task CreateConversationAsync(Conversation conversation)
        {
            _appDbContext.Add(conversation);
            await SaveChangesAsync();
        }

        public async Task DeleteConversationAsync(Conversation conversation)
        {
            _appDbContext.Remove(conversation);
            await SaveChangesAsync();

        }

        public async Task<IEnumerable<Conversation>> GetAllConversationsAsync()
        {
            return await _appDbContext.Conversations.Include(c => c.Sender).Include(c => c.Reciever).ToListAsync();
        }

        public async Task<IEnumerable<Conversation>> GetconversationBetweenTwoUsersAsync(string senderId, string recieverId)
        {
            return await _appDbContext.Conversations
                .Where(c => String.Equals(c.SenderId, senderId) && String.Equals(c.RecieverId, recieverId)
                || String.Equals(c.SenderId, recieverId) && String.Equals(c.RecieverId, senderId))
                .ToListAsync();

        }

        public async Task<Conversation> GetConversationByIdAsync(int id)
        {
            return await _appDbContext.Conversations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllConversationsAsync()
        {
            var conversations = await _appDbContext.Conversations.ToListAsync();

            foreach(var conv in conversations)
            {
                _appDbContext.Remove(conv);
            }

            await SaveChangesAsync();
        }
    }
}
