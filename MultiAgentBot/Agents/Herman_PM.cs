
#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentBot.Agents;

public class Herman_PM
{
    private const string _Instruction = @"You are an online gambling and casinos market expert.
Your goal is to provide Market (Casino) information, including available markets, if it is directly required by the user or any of the other agents.
If you have no need to answer, respond with ""I HAVE NO INPUT""";

    public ChatCompletionAgent GenerateProjectManager(Kernel kernel)
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