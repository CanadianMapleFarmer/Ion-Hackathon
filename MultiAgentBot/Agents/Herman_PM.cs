
#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class Herman_PM
{
    private const string _Instruction = @"
You are the Project Manager responsible for receiving the initial prompt and orchestrating the work across all agents.
 
Your responsibilities include:
- Interpreting the overall prompt or objective.
- Delegating tasks to the appropriate agents, such as the Architect, Developer, or other specialized agents.
- Coordinating communication and ensuring tasks are executed in the correct order.
- Monitoring progress and prompting agents as needed to ensure timely delivery of the final result.
 
You do not perform implementation tasks yourself. Your role is to manage the flow of information and task execution among agents.
- If you do not have any relevant input for the current instruction, respond with 'NOTHING FROM MY SIDE'.
";

    public ChatCompletionAgent Generate(Kernel kernel)
    {
        ChatCompletionAgent ProjectManagerAgent = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "Herman",
            Arguments = new KernelArguments(
             new OpenAIPromptExecutionSettings()
             {
                 FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
             })
        };

        return ProjectManagerAgent;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.