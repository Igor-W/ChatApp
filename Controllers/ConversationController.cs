using AutoMapper;
using ChatApp.DTOs;
using ChatApp.Entities;
using ChatApp.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/conversation")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;
        public ConversationController(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversationReadDTO>>> GetAllConversations()
        {
            var conversations = await _conversationRepository.GetAllConversationsAsync();


            if (conversations != null)
            {

                return Ok(_mapper.Map<IEnumerable<ConversationReadDTO>>(conversations));
            }

            else
                return NotFound();
        }

        [HttpGet("{id}", Name = "GetConversationById")]
        public async Task<ActionResult<IEnumerable<ConversationReadDTO>>> GetConversationById(int id)
        {
            var conversation = await _conversationRepository.GetConversationByIdAsync(id);


            if (conversation != null)
                return Ok(_mapper.Map<IEnumerable<ConversationReadDTO>>(conversation));
            else
                return NotFound();
        }

        [HttpGet("{senderId}&&{recieverId}")]
        public async Task<ActionResult<IEnumerable<ConversationReadDTO>>> GetConversationBetweenTwoUsers(string senderId, string recieverId)
        {

            var conversations = await _conversationRepository.GetconversationBetweenTwoUsersAsync(senderId, recieverId);

            return Ok(_mapper.Map<IEnumerable<ConversationReadDTO>>(conversations));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConversation(int id)
        {
            var conv = await _conversationRepository.GetConversationByIdAsync(id);

            if (conv == null)
            {
                return NotFound();
            }
            else
            {
                await _conversationRepository.DeleteConversationAsync(conv);
                return NoContent();
            }
        }
        [HttpDelete]
        [Route("delete-all")]
        public async Task DeleteAllConversations()
        {
            await _conversationRepository.DeleteAllConversationsAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ConversationReadDTO>> CreateConversation(ConversationCreateDTO conv)
        {
            var convToCreate = _mapper.Map<Conversation>(conv);
            await _conversationRepository.CreateConversationAsync(convToCreate);

            var convToReturn = _mapper.Map<ConversationReadDTO>(convToCreate);

            return CreatedAtRoute(nameof(GetConversationById), new { Id = convToReturn.Id }, convToReturn);
        }

    }
}
