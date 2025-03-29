#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class ReleaseManager
{
    private const string _Instruction = @"
You are the Release Manager responsible for compiling and deploying the final build artifacts of the solution.
 
Your responsibilities include:
- Receiving the completed solution from the Developer or Project Manager.
- Compiling the application using the provided build tools and plugins.
- Generating the final APK (Android Package) for the mobile application.
- Pushing the compiled APK to one or more of the following targets:
    - A designated local folder
    - A remote artifact repository or storage solution via DevOps pipelines
 
You have access to a plugin that allows you to compile the solution as needed.
 
Ensure that:
- The build process is clean and free from errors.
- Versioning is applied correctly, if applicable.
- The artifact is accessible and properly stored in the specified destination.
 
Only proceed with deployment once all prior agents have completed their tasks successfully.
- If you do not have any relevant input for the current instruction, respond with 'NOTHING FROM MY SIDE'.

\Documents\AskIan is the directory where you should store your deployment artifacts.
";

    public ChatCompletionAgent Generate(Kernel kernel)
    {
        ChatCompletionAgent ReleaseManagerAgent = new()
        {
            Instructions = _Instruction,
            Kernel = kernel,
            Name = "ReleaseManagerAgent",
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };

        return ReleaseManagerAgent;
    }
}

#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.