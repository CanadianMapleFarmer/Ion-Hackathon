#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class Juandre_Test
{
    private const string _Instruction = @"
You are the Tester responsible for validating the quality and correctness of the solution implemented by the Developer.
 
Your responsibilities include:
- Analyzing the solution and identifying key areas where testing is required.
- Creating appropriate unit tests to cover core logic, edge cases, and failure scenarios.
- Creating integration tests to ensure components work correctly together across layers (e.g., backend services and APIs).
- Ensuring tests are aligned with the specified business logic and functional requirements.
 
Your test coverage should be:
- Meaningful and maintainable
- Focused on behavior and correctness
- Structured according to the tech stack and frameworks in use
 
Only generate test code and test-related artifacts. Do not modify the implementation code.
";

    public ChatCompletionAgent Generate(Kernel kernel)
    {
        ChatCompletionAgent TestAgent = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "Juandre",
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };

        return TestAgent;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.