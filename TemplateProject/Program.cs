
int[] nums = [1, 2, 3, 4, 5];
var (max, avg) = CalculateMaxAndAverage(nums);
Console.WriteLine($"Max: {max}, Average: {avg}");


(int num1, string msg) CalculateMaxAndAverage(int[] nums)
{
    int max = nums.Max();
    double avg = nums.Average();
    return (max, "Avg:"+avg);
}

///编译器报错的代码
//static async Task<string> FetchStringAsync(ReadOnlySpan<char> requestUrl)
//{
//    HttpClient client = new HttpClient();
//    var task = client.GetStringAsync(requestUrl.ToString());
//    return await task;
//}



///正常编译的代码
static async Task<string> FetchStringAsync(ReadOnlyMemory<char> requestUrl)
{
    HttpClient client = new HttpClient();
    var task = client.GetStringAsync(requestUrl.ToString());
    return await task;
}

struct SomeStruct
{
    public SomeStruct(int num)
    {
        Num = num;
    }
    public int Num { get; private set; }
}

class SomeClass
{
    private int num;
    public SomeClass(SomeStruct someStruct)
    {
        SomeStruct = someStruct;
        num = SomeStruct.Num;
    }
    SomeStruct SomeStruct { get; set; }

    internal ref int GetRefValue(SomeClass someClass)
    {
        return ref num;
    }
}
