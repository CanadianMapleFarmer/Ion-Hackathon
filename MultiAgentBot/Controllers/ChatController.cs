using MultiAgentBot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
namespace MultiAgentBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;

        private AgentGroupChat _chat;

        public ChatController(ILogger<ChatController> logger, AgentGroupChat chat)
        {
            _logger = logger;
            _chat = chat;
        }

        [HttpPost(Name = "StartWork")]
        public async Task<string> StartWork([FromBody] QueryRequestModel query)
        {
            _chat.AddChatMessage(new Microsoft.SemanticKernel.ChatMessageContent(AuthorRole.User, query.Value));
            StringBuilder responses = new StringBuilder();
            await foreach (var item in _chat.InvokeAsync())
            {
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                responses.AppendLine($"{item.Role} - {item.AuthorName ?? "*"}: '{item.InnerContent}'");
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            }
            return responses.ToString();
        }
    }
}
#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.