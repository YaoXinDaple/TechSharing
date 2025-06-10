## 代理模式
代理模式有三种类型：远程代理、虚拟代理和保护代理。
远程代理用于访问远程对象，虚拟代理用于延迟加载资源，保护代理用于控制对对象的访问。

示例代码（Book Parser）:
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public interface IBookParser
{
	List<string> Parse(string filePath);
}

public class BookParser : IBookParser
{
	public List<string> Parse(string filePath)
	{
		if (!File.Exists(filePath))
			throw new FileNotFoundException("File not found", filePath);
		var content = File.ReadAllText(filePath);
		return Regex.Matches(content, @"\b\w+\b").Select(m => m.Value).ToList();
	}
}

public class BookParserProxy : IBookParser
{
	private readonly string _filePath;
	private BookParser _bookParser;
	public BookParserProxy(string filePath)
	{
		_filePath = filePath;
	}
	public List<string> Parse(string filePath)
	{
		if (_bookParser == null)
		{
			_bookParser = new BookParser();
		}
		return _bookParser.Parse(_filePath);
	}
}

public class Program
{
	public static void Main(string[] args)
	{
		var filePath = "book.txt"; // 替换为实际文件路径
		var parser = new BookParserProxy(filePath);
		try
		{
			var words = parser.Parse(filePath);
			Console.WriteLine($"Parsed {words.Count} words from the book.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}
}
```
BookParserProxy 类是 BookParser 的代理类，它延迟加载 BookParser 实例，直到需要解析文件时才创建它。这样可以节省资源，尤其是在处理大型文件时。
BookParser.Parser 代表任何可能非常昂贵的操作，如网络请求或数据库查询。代理模式可以用于控制对这些昂贵操作的访问，提供缓存或延迟加载等功能。