

string rateTypes = "0.015,0.03,0.01,0.06,0.09,0.15";
string rateTypes2 = "0.03,0.01,0.06,0.015,0.09,0.15";
string rateTypes3 = "0.03,0.01,0.06,0.09,0.15,0.015";

string rateTypes4 = "0.015,0.03,0.01,0.06,0.09,0.15,0.015";
string rateTypes5 = "0.015,0.03,0.01,0.015,0.06,0.09,0.15";
string rateTypes6 = "0.03,0.01,0.015,0.06,0.09,0.15,0.015";

Console.WriteLine(UseSpan(rateTypes));

//使用LinQPad运行

/*
 

//string s = "Hello C# world!";

//var result = s.Split(' ');

//var result = s.Split(' ',2);//使用分隔符将原始字符串分割为两部分


=================================================================================================


//string s = "Hello ,    C# ,,   world!";

//var result = s.Split(',',  StringSplitOptions.RemoveEmptyEntries);//如果两个分隔符之间没有字符，结果中不会返回这部分

//var result = s.Split(',',StringSplitOptions.TrimEntries);//返回结果中的每个元素都会去掉前后的空格


//result.Dump();
*/


//字符串的构造函数

//string str = new string('=', 20);


//string s= "Hello world!";
//string.Join("", s.Reverse().ToArray());


//char[] chars = { 'h', 'e', 'l', 'l','o',',','w','o','r','l','d','!' };
//var reversedArr = chars.Reverse().ToArray();
//string str2 = new string(reversedArr);

Console.ReadKey();


 string UseSpan(string InputString)
{
    var span = InputString.AsSpan();
    var emptySpan = new Span<char>(new char[span.Length]);

    var matchs = new string[] { "0.015,", ",0.015", "0.015" };

    foreach (var m in matchs)
    {
        int startIndex = span.IndexOf(m);
        if (startIndex == 0)
        {
            span.Slice(m.Length).CopyTo(emptySpan);
        }
        else if (startIndex > 0 && span.Length == startIndex + m.Length)
        {
            if (span.Length == startIndex + m.Length)
            {
                span.Slice(0, startIndex).CopyTo(emptySpan);
            }
            else
            {
                var leftPart = span.Slice(0, startIndex);
                var rightPart = span.Slice(startIndex + m.Length);
                leftPart.CopyTo(emptySpan);
                rightPart.CopyTo(emptySpan);
            }
        }
    }
    return emptySpan.ToString();
}