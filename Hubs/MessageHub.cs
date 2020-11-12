using AutoMapper;
using ChatApp.Controllers;
using ChatApp.DTOs;

using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;



namespace ChatApp.Hubs
{
    public class MessageHub : Hub
    {
        //public async Task New Message(Message msg)
        //{
        //    await Clients.All.SendAsync("MessageReceived", msg);
        //}

        private static readonly Dictionary<string, string> _users = new Dictionary<string, string>();

        private ConversationController _conversationController;
        private IMapper _mapper;
        private UserController _userController;

        public MessageHub(ConversationController conversationController, IMapper mapper, UserController userController)
        {
            _conversationController = conversationController;
            _mapper = mapper;
            _userController = userController;
        }
        public async Task SendMessage(string senderId, ConversationReadDTO message)
        {
            Debug.WriteLine("-----------------------------");
            Debug.WriteLine("message:");
            Debug.WriteLine(message.Message);
            Debug.WriteLine("senderId:");
            Debug.WriteLine(message.SenderId);
            Debug.WriteLine("status:");
            Debug.WriteLine(message.Status);
            Debug.WriteLine("recieverId:");
            Debug.WriteLine(message.RecieverId);
            Debug.WriteLine("createdAt:");
            Debug.WriteLine(message.CreatedAt);
            Debug.WriteLine("-----------------------------");
            Debug.WriteLine(senderId);
            




            await Clients.All.SendAsync("ReceiveMessage", message);
            await _conversationController.CreateConversation(_mapper.Map<ConversationCreateDTO>(message));






        }

        public override Task OnConnectedAsync()
        {
        
            return base.OnConnectedAsync();
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}