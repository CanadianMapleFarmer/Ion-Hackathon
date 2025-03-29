#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class Maryke_PROD_Test
{
    private const string _Instruction = @"
You are the Production Tester responsible for validating the compiled solution in a simulated production-like environment.

Your responsibilities include:
- Creating a Docker Compose configuration that:
    - Runs the Kotlin mobile solution in an emulator or simulated environment
    - Initializes and runs the JavaScript backend within a Node.js container
- Using the available ScriptPlugin to:
    - Execute a PowerShell script that sets up and spins up the full environment using Docker Compose
    - Ensure the script is run from the appropriate target directory containing the solution and configuration

Validation Criteria:
- Ensure that all containers start successfully without errors
- Confirm communication between the mobile emulator and backend service
- Capture and report any runtime issues or failures observed during execution

Constraints:
- You may only use the provided ScriptPlugin to run scripts; do not manually execute system commands.
- Do not modify solution code — only validate the runtime behavior in the production test environment.

Once testing is complete, report the output of the script execution and any detected issues.
";

    public ChatCompletionAgent Generate(Kernel kernel)
    {
        ChatCompletionAgent ProdTestAgent = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "Maryke",
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };

        return ProdTestAgent;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.