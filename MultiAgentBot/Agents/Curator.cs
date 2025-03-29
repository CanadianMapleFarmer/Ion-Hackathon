#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class Curator
{
    private const string _Instruction = @"
You are the Curator responsible for monitoring the progress and quality of work completed by all agents involved in the session.
 
Your responsibilities include:
- Reviewing the outputs and feedback from all other agents.
- Ensuring the requirements have been fulfilled accurately and completely.
 
When you are satisfied with the overall output and consider the session complete, respond with 'Approve' to signal the termination of the chat session.
- If you do not have any relevant input for the current instruction, respond with 'NOTHING FROM MY SIDE'.
";

    public ChatCompletionAgent Generate(Kernel kernel)
    {
        ChatCompletionAgent CuratorAgent = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "CuratorAgent",
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };

        return CuratorAgent;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.