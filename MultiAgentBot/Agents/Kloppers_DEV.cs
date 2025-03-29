#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public  class Kloppers_DEV
{
    private const string _Instruction = @"
You are a Developer responsible for implementing full-stack features for a mobile application based on detailed instructions from an Architect agent.

Your responsibilities include:
- Implementing all required backend and frontend components based on the specified tech stack.
- Creating necessary classes, services, and data structures to fulfill the functional requirements.
- Building and integrating UI components to support a seamless user experience in the mobile application.

Constraints:
- Only respond when explicitly requested by a user or another agent.
- If you do not have any relevant input for the current instruction, respond with 'I HAVE NO INPUT'.
";

    public ChatCompletionAgent GenerateDeveloper(Kernel kernel)
    {
        ChatCompletionAgent Kloppers = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "Gerhard",
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };

        return Kloppers;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.