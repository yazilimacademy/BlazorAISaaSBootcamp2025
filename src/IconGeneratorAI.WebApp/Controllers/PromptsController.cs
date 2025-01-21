using IconGeneratorAI.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace IconGeneratorAI.WebApp.Controllers
{
    [Route("api/prompts")]
    [ApiController]
    public class PromptsController : ControllerBase
    {

        [HttpGet("improve")]
        public async Task<IActionResult> ImprovePromptAsync(string prompt)
        {
            // 1) Create kernel builder.
            var builder = Kernel.CreateBuilder();

            // 3) Add the "gemma2:latest" for Turkish translation.
            builder.AddOllamaChatCompletion(
           modelId: "gemma2:latest",
           endpoint: new Uri("http://localhost:11434"),
           serviceId: "promptImprovementService"
           );

            //     builder.AddOllamaChatCompletion(
            //   modelId: "deepseek-r1:1.5b",
            //   endpoint: new Uri("http://localhost:11434"),
            //   serviceId: "promptReasoningService"
            //   );

            var kernel = builder.Build();

            var improvementService = kernel.GetRequiredService<IChatCompletionService>("promptImprovementService");


            //var reasoningService = kernel.GetRequiredService<IChatCompletionService>("promptReasoningService");

            var chatMessages = new ChatHistory("You are a helpful prompt improver assisant. Improve the prompt to be more descriptive and detailed. **Do not add any other text or information. Do not give me the response in markdown just give me the text.**");

            chatMessages.AddUserMessage(prompt);

            // 9) Invoke the translator to get the Turkish text.
            var translationReply = await improvementService.GetChatMessageContentAsync(chatMessages);

            Console.WriteLine(translationReply.Content);

            return Ok(new GetPromptResponseDto(translationReply.Content));

        }
    }
}
