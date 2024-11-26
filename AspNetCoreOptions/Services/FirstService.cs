using AspNetCoreOptions.Options;
using Microsoft.Extensions.Options;

namespace AspNetCoreOptions.Services;

public class FirstService
{
    private readonly AlphaOption _alphaOptions;
    private readonly BetaOption _betaOptions;

    public FirstService(IOptions<AlphaOption> alphaOptions, IOptions<BetaOption> betaOptions)
    {
        _alphaOptions = alphaOptions.Value;
        _betaOptions = betaOptions.Value;
    }

    public void DoWork()
    {
        Console.WriteLine($"Alpha: {_alphaOptions.Name}, {_alphaOptions.Identifier}");
        Console.WriteLine($"Beta: {_betaOptions.Count}, {_betaOptions.Interval}");
    }   
}