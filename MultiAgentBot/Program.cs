using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MultiAgentBot.classes;
using MultiAgentBot.plugins;
using OpenAI;
using System.ClientModel;
#pragma warning disable SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(lb => lb.AddConsole().SetMinimumLevel(LogLevel.Trace));

builder.Services.AddSingleton(sp =>
{
    var handler = new HttpClientHandler();
    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
    handler.ServerCertificateCustomValidationCallback =
        (httpRequestMessage, cert, cetChain, policyErrors) =>
        {
            Thread.Sleep(100);
            return true;
        };

    var openAiBaseHttpClient = new HttpClient(handler);
    openAiBaseHttpClient.Timeout = TimeSpan.FromSeconds(120);

    var clientOptions = new OpenAIClientOptions();
    //clientOptions.RetryPolicy = new RetryPolicy(maxRetries: 1, DelayStrategy.CreateFixedDelayStrategy(TimeSpan.FromMinutes(4)));
    //clientOptions.Retry.MaxRetries = 1;
    //clientOptions.Retry.NetworkTimeout = TimeSpan.FromMinutes(4);
    clientOptions.Endpoint = new Uri("https://ai-rynardt-eastus-test.openai.azure.com/");

    var openAIClient = new OpenAIClient(
        new ApiKeyCredential("e9f24b40c7b34498b245b470c4917d19"), clientOptions);

    var aopenAIClient = new AzureOpenAIClient(
        new Uri("https://ai-rynardt-eastus-test.openai.azure.com/"),
        new ApiKeyCredential("e9f24b40c7b34498b245b470c4917d19"));

    IKernelBuilder builder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion("gpt-4o", aopenAIClient);
    builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole().SetMinimumLevel(LogLevel.Trace));
    var kernel = builder.Build();
    kernel.LoggerFactory.CreateLogger("KernelLogger");

    return kernel;
});

builder.Services.AddScoped(sp =>
{
    var kernel = sp.GetRequiredService<Kernel>();
    var marketAgentKernel = kernel.Clone();
    //marketAgentKernel.CreatePluginFromType<MarketPlugin>();
    marketAgentKernel.Plugins.AddFromType<MarketPlugin>();
    var requirementAgentKernel = kernel.Clone();
    //requirementAgentKernel.CreatePluginFromType<RequirementPlugin>();
    requirementAgentKernel.Plugins.AddFromType<RequirementPlugin>();
    string projectManager = @"You are an online gambling and casinos market expert.
Your goal is to provide Market (Casino) information, including available markets, if it is directly required by the user or any of the other agents.
If you have no need to answer, respond with ""I HAVE NO INPUT""";

    string businessAnalyst = @"You are an expert software business analyst and you enjoy turning simple user prompts about the problems or into detailed and well structured business requirements documents.
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
    If you have no need to answer, respond with ""I HAVE NO INPUT""";

    string architect = @"You are a Business Analyst who only focusses on agnostic detailed requirements, 
your goal is to provide agnostic detailed requirements under agnostic requirements for any market, 
if it is directly required by the user or any of the other agents.
If you have no need to answer, respond with ""I HAVE NO INPUT""";

    string uiDeveloper = @"You are a UI developer who only focuses on generating code from given business requirements,
that are supplied by the Business Analyst, and integrate with the backend developer. If you have no need to answer, respond with ""I HAVE NO INPUT""";

    string backendDeveloper = @"You are a Business Analyst who only focusses on detailed requirements for a market, 
your goal is to provide detailed requirements under requirements for a specific market, 
if it is directly required by the user or any of the other agents.
If you have no need to answer, respond with ""I HAVE NO INPUT""";

    string releaseManager = @"You are an expert on regulations for online gambling and casinos. 
Your goal is to provide details around regulations for a given market, if it is directly required by the user or any of the other agents.
If you have no need to answer, respond with ""I HAVE NO INPUT""";
    
    string curatorAgent = @"XYZ";

    ChatCompletionAgent ProjectManagerAgent = new()
    {
        Instructions = projectManager,
        Kernel = marketAgentKernel,
        Name = "Herman",
        Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
    };


    ChatCompletionAgent BusinessAnalystAgent = new()
    {
        Instructions = businessAnalyst,
        Kernel = kernel,
        Name = "Mason",
        Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
    };

    ChatCompletionAgent ArchitectAgent = new()
    {
        Instructions = architect,
        Kernel = kernel,
        Name = "Ian",
        Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
    };

    ChatCompletionAgent UIDeveloperAgent = new()
    {
        Instructions = uiDeveloper,
        Kernel = kernel,
        Name = "Keelan",
        Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
    };

    ChatCompletionAgent BackendDevelopmentAgent = new()
    {
        Instructions = backendDeveloper,
        Kernel = requirementAgentKernel,
        Name = "Gerhard",
        Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
    };

    ChatCompletionAgent ReleaseManagerAgent = new()
    {
        Instructions = releaseManager,
        Kernel = kernel,
        Name = "Rynardt",
        Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
    };
    
    ChatCompletionAgent CuratorManager = new()
    {
        Instructions = curatorAgent,
        Kernel = kernel,
        Name = "Curator",
        Arguments = new KernelArguments(
            new OpenAIPromptExecutionSettings()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            })
    };

    AgentGroupChat chat = new(
        ProjectManagerAgent,
        BusinessAnalystAgent,
        ArchitectAgent,
        UIDeveloperAgent,
        BackendDevelopmentAgent,
        ReleaseManagerAgent
        )
    {
        ExecutionSettings = new()
        {
            TerminationStrategy = new ApprovalTerminationStrategy()
            {
                Agents = [CuratorManager],
                MaximumIterations = 20
            }
        }
    };

    return chat;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0040 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.