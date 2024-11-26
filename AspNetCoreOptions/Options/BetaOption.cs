using System.ComponentModel.DataAnnotations;

namespace AspNetCoreOptions.Options;
public class BetaOption
{
    [Range(1, int.MaxValue)]
    public int Count { get; set; }
    public int Interval { get; set; }
}