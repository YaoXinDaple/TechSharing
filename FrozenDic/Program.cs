using Newtonsoft.Json;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

var normalDic = new Dictionary<int, int>();
normalDic.Add(1, 1);
normalDic.Add(2, 2);

var immutableDic = normalDic.ToImmutableDictionary();
immutableDic = immutableDic.Add(3, 3);

var readonlyDic = new ReadOnlyDictionary<int, int>(normalDic);
normalDic[1] = 3;

var frozenDic = normalDic.ToFrozenDictionary();
//frozenDic.Add(4, 4);


Console.WriteLine($"normalDic:{JsonConvert.SerializeObject(normalDic)}");
Console.WriteLine($"immutableDic:{JsonConvert.SerializeObject(immutableDic)}");
Console.WriteLine($"readonlyDic:{JsonConvert.SerializeObject(readonlyDic)}");
Console.WriteLine($"frozenDic:{JsonConvert.SerializeObject(frozenDic)}");
Console.ReadKey();
