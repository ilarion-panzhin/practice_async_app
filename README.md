# Concurrency Lab (.NET) — small steps

A tiny learning project to understand:
- multithreading
- async/await
- concurrency vs parallelism

## Learning style
You asked for small steps. This repo is set up for that.

## Step 1 (current): minimal skeleton
Goal: run one scenario and read logs with thread IDs + timings.

Planned CLI style:
- `dotnet run -- sync-vs-async`
- later: `cpu-vs-io`, `race`, `lock`, `throttling`, `cancellation`

## Weekend roadmap (small increments)

### Day 1
1. Sync vs Async
2. CPU-bound vs I/O-bound
3. Race condition (broken on purpose)

### Day 2
4. Fix race (`lock`, `Interlocked`)
5. Throttling (`SemaphoreSlim`)
6. Cancellation (`CancellationToken`)

## Suggested workflow per scenario
For each scenario, write down:
1. what you expect
2. what actually happened
3. why

That reflection is where most learning happens.

## Prerequisites
- .NET 8 SDK (`dotnet --version`)

## Next step for you
After installing .NET SDK:
1. `dotnet run --project src/ConcurrencyLab -- sync-vs-async`
2. Observe thread IDs before and after `await`.
3. Compare total duration for sequential vs concurrent execution.
