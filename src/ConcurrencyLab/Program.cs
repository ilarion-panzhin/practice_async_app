using ConcurrencyLab.Infrastructure;
using ConcurrencyLab.Scenarios;

var logger = new AppLogger();

var scenarios = new Dictionary<string, IScenario>(StringComparer.OrdinalIgnoreCase)
{
    ["sync-vs-async"] = new SyncVsAsyncScenario(logger),
};

if (args.Length == 0)
{
    logger.Info("Usage: dotnet run -- <scenario>");
    logger.Info("Available scenarios:");
    foreach (var key in scenarios.Keys.OrderBy(x => x))
    {
        logger.Info($"  - {key}");
    }
    return;
}

var scenarioName = args[0];
if (!scenarios.TryGetValue(scenarioName, out var scenario))
{
    logger.Info($"Unknown scenario: {scenarioName}");
    return;
}

await scenario.RunAsync(CancellationToken.None);
