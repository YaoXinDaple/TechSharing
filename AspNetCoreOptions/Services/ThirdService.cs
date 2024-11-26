using AspNetCoreOptions.Options;
using Microsoft.Extensions.Options;

namespace AspNetCoreOptions.Services;
public class ThirdService
{
    private readonly IOptionsMonitor<AlphaOption> _alphaOptions;
    private readonly IOptionsMonitor<BetaOption> _betaOptions;
    public ThirdService(IOptionsMonitor<AlphaOption> alphaOptions, IOptionsMonitor<BetaOption> betaOptions)
    {
        _alphaOptions = alphaOptions;
        _betaOptions = betaOptions;
    }

    public void DoWork()
    {
        Console.WriteLine($"Alpha: {_alphaOptions.CurrentValue.Name}, {_alphaOptions.CurrentValue.Identifier}");
        Console.WriteLine($"Beta: {_betaOptions.CurrentValue.Count}, {_betaOptions.CurrentValue.Interval}");
    }
}