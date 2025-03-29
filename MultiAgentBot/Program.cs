using System;
using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using MultiAgentBot.classes;
using MultiAgentBot.Plugins;
using OpenAI;
using System.ClientModel;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MultiAgentBot.Agents;

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
    kernel.Plugins.AddFromType<FilePlugin>();
    
    var projectManagerAgent = new Herman_PM().Generate(kernel);
    var businessAnalystAgent = new Mason_BA().Generate(kernel);
    var developerAgent = new Kloppers_DEV().Generate(kernel);
    var architectAgent = new Ian_ARCH().Generate(kernel);
    var curatorAgent = new Curator().Generate(kernel);
    var releaseManagerAgent = new ReleaseManager().Generate(kernel);

    AgentGroupChat chat = new(
        projectManagerAgent,
        businessAnalystAgent,
        architectAgent,
        developerAgent,
        releaseManagerAgent
        )
    {
        ExecutionSettings = new()
        {
            TerminationStrategy = new ApprovalTerminationStrategy()
            {
                Agents = [curatorAgent],
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