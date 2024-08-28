
//使用LinQPad运行

/*
 

//string s = "Hello C# world!";

//var result = s.Split(' ');

//var result = s.Split(' ',2);


=================================================================================================


//string s = "Hello ,    C# ,    world!";

//var result = s.Split(',',  StringSplitOptions.RemoveEmptyEntries);

//var result = s.Split(',',StringSplitOptions.TrimEntries);


//result.Dump();
*/


//字符串的构造函数
string str = new string('=', 20);


string s= "Hello world!";
string.Join("", s.Reverse().ToArray());


char[] chars = { 'h', 'e', 'l', 'l','o',',','w','o','r','l','d','!' };
var reversedArr = chars.Reverse().ToArray();
string str2 = new string(reversedArr);


//字符串的包含
/*
 * Contains
 * IndexOf
 * Any()
 */
