using System.ComponentModel.DataAnnotations;

namespace AspNetCoreOptions.Options;

public class AlphaOption
{
    [Required]
    public string Name{get;set;}

    [Required]
    public string Identifier{get;set;}

}