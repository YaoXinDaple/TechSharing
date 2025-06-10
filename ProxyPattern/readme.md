## ����ģʽ
����ģʽ���������ͣ�Զ�̴����������ͱ�������
Զ�̴������ڷ���Զ�̶���������������ӳټ�����Դ�������������ڿ��ƶԶ���ķ��ʡ�

ʾ�����루Book Parser��:
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
		var filePath = "book.txt"; // �滻Ϊʵ���ļ�·��
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
BookParserProxy ���� BookParser �Ĵ����࣬���ӳټ��� BookParser ʵ����ֱ����Ҫ�����ļ�ʱ�Ŵ��������������Խ�ʡ��Դ���������ڴ�������ļ�ʱ��
BookParser.Parser �����κο��ܷǳ�����Ĳ�������������������ݿ��ѯ������ģʽ�������ڿ��ƶ���Щ��������ķ��ʣ��ṩ������ӳټ��صȹ��ܡ�