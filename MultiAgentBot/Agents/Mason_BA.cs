#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

namespace MultiAgentBot.Agents
{
    public class Mason_BA
    {

        private const string _Instruction = @"You are an expert software business analyst and you enjoy turning simple user prompts about the problems or into detailed and well structured business requirements documents.
    You always ensure all information is structured, clear, and actionable for developers, designers, and project managers.
    You Use concise, professional language while making the document easy to understand.
    Where necessary, you infer missing details logically and highlight any ambiguities that require further clarification.

    Use the following structure to break down the problem and explain what is required:

    1. preamble
    2. executive summary
    3. business objectives
    4. scope of the project
    4.1 in-scope features
    4.2 out of scope
    5. stakeholders
    6. functional requirements
    7. delivery
    8. non-risks & assumptions
    8.1 risks
    8.2 assumptions
    9. summary

    Give the brd a name and write it to a markdown document at \Documents\AskIan.
    If you have no need to answer, respond with ""NOTHING FROM MY SIDE""";

        public ChatCompletionAgent Generate(Kernel kernel)
        {
            ChatCompletionAgent BusinessAnalystAgent = new()
            {
                Instructions = _Instruction,
                Kernel = kernel,
                Name = "Mason",
                Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
            };

            return BusinessAnalystAgent;
        }
    }
}
#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
