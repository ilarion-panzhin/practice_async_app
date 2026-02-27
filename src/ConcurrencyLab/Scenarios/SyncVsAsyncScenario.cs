using ConcurrencyLab.Infrastructure;
using System.Diagnostics;

namespace ConcurrencyLab.Scenarios;

public sealed class SyncVsAsyncScenario(AppLogger logger) : IScenario
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        logger.Info("Scenario: sync-vs-async");

        logger.Info("Sequential: 5 x 1s simulated I/O (await one by one)");
        var sequentialSw = Stopwatch.StartNew();
        for (var i = 1; i <= 5; i++)
        {
            await SimulatedIoAsync($"seq-{i}", cancellationToken);
        }
        sequentialSw.Stop();

        logger.Info("Concurrent: 5 x 1s simulated I/O (Task.WhenAll)");
        var concurrentSw = Stopwatch.StartNew();
        var tasks = Enumerable.Range(1, 5)
            .Select(i => SimulatedIoAsync($"con-{i}", cancellationToken));
        await Task.WhenAll(tasks);
        concurrentSw.Stop();

        logger.Info($"Sequential total: {sequentialSw.ElapsedMilliseconds} ms");
        logger.Info($"Concurrent total: {concurrentSw.ElapsedMilliseconds} ms");
        logger.Info("Learning point: async I/O waiting does not require a dedicated blocked thread.");
    }

    private async Task SimulatedIoAsync(string name, CancellationToken cancellationToken)
    {
        logger.Info($"{name}: before await Task.Delay");
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        logger.Info($"{name}: after await Task.Delay");
    }
}
