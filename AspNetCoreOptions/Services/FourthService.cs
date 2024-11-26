//在这个类中，演示 IOptionsSnapshot 和 IOptionsMonitor 的区别

using AspNetCoreOptions.Options;
using Microsoft.Extensions.Options; 

namespace AspNetCoreOptions.Services;

public class FourthService
{
    private readonly AlphaOption _alphaOptions;
    private readonly BetaOption _betaOptions;

    public FourthService(IOptionsMonitor<AlphaOption> alphaOptions, IOptionsMonitor<BetaOption> betaOptions)
    {
        _alphaOptions = alphaOptions.CurrentValue;
        _betaOptions = betaOptions.CurrentValue;
    }   

    public void DoWork()
    {
        Console.WriteLine($"Alpha: {_alphaOptions.Name}, {_alphaOptions.Identifier}");
        Console.WriteLine($"Beta: {_betaOptions.Count}, {_betaOptions.Interval}");
    }   
}