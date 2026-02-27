using ConcurrencyLab.Infrastructure;
using System.Diagnostics;

namespace ConcurrencyLab.Scenarios;

public sealed class SyncVsAsyncScenario(AppLogger logger) : IScenario
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        logger.Info("Scenario: sync-vs-async");

        logger.Info("Sync blocking: 5 x 1s using Thread.Sleep (blocks current thread)");
        var blockingSw = Stopwatch.StartNew();
        for (var i = 1; i <= 5; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SimulatedIoSync($"sync-{i}");
        }
        blockingSw.Stop();

        logger.Info("Async concurrent: 5 x 1s simulated I/O (Task.Delay + Task.WhenAll)");
        var asyncSw = Stopwatch.StartNew();
        var tasks = Enumerable.Range(1, 5)
            .Select(i => SimulatedIoAsync($"async-{i}", cancellationToken));
        await Task.WhenAll(tasks);
        asyncSw.Stop();

        logger.Info($"Sync blocking total: {blockingSw.ElapsedMilliseconds} ms");
        logger.Info($"Async concurrent total: {asyncSw.ElapsedMilliseconds} ms");
        logger.Info("Learning point: synchronous blocking occupies a thread; async waiting can free it.");
    }

    private void SimulatedIoSync(string name)
    {
        logger.Info($"{name}: before Thread.Sleep");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        logger.Info($"{name}: after Thread.Sleep");
    }

    private async Task SimulatedIoAsync(string name, CancellationToken cancellationToken)
    {
        logger.Info($"{name}: before await Task.Delay");
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        logger.Info($"{name}: after await Task.Delay");
    }
}
