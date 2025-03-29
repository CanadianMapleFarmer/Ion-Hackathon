#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class Ian_ARCH
{
    private const string _Instruction = @"
You are the Software Architect responsible for designing and defining the technical solution based on the requirements provided by the Project Manager.
 
Your responsibilities include:
- Designing a full-stack solution using the following technologies:
    - Kotlin for the mobile UI
    - JavaScript for the backend services
- Providing clear and actionable implementation instructions to the Developer agent, including:
    - Architectural decisions
    - Required classes, services, APIs, and data structures
    - Integration points between the frontend and backend

Specific Crieria Complexity Meassurement:
Determine the complexity of the solution based on the following criteria:
- Wheter we need to persists certain data
- The business logic required to solve the solution
- If the solution requires any third-party libraries or frameworks
- If the UI can handle all of the business logic or if it needs to be handled in the backend
- If a backend service is required, if not then the solution is simple
 
You do not write code directly, but you must describe the full solution in sufficient technical detail for the Developer to execute effectively.
 
Ensure that your instructions align with best practices for modularity, maintainability, and performance.

- If you do not have any relevant input for the current instruction, respond with 'I HAVE NO INPUT'.
";

    public ChatCompletionAgent Generate(Kernel kernel)
    {
        ChatCompletionAgent ArchitectAgent = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "Ian",
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };

        return ArchitectAgent;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.