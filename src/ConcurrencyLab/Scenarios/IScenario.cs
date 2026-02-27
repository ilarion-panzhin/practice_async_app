namespace ConcurrencyLab.Scenarios;

public interface IScenario
{
    Task RunAsync(CancellationToken cancellationToken);
}
