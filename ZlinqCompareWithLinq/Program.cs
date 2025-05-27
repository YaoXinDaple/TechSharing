// See https://aka.ms/new-console-template for more information
using ZlinqCompareWithLinq;

Console.WriteLine("Hello, World!");



BenchmarkDotNet.Running.BenchmarkRunner.Run<LinqAllocateBenchmark>();

Console.ReadLine();

//创建10个GLProjectType 实例
var projectTypes = new List<GLProjectType>();
for (int i = 0; i < 10; i++)
{
    projectTypes.Add(new GLProjectType
    {
        ProjectTypeCode = $"Code{i}",
        ProjectTypeName = $"Name{i}"
    });
}


//为 projectTypes 中每个实例生成 10-50个 GLProject 实例
var projects = new List<GLProject>();
for (int i = 0; i < projectTypes.Count; i++)
{
    var projectType = projectTypes[i];
    var projectCount = new Random().Next(10, 50);
    for (int j = 0; j < projectCount; j++)
    {
        projects.Add(new GLProject
        {
            ProjectTypeCode = projectType.ProjectTypeCode,
            ProjectName = $"{projectType.ProjectTypeCode}-ProjectName{j}",
            ProjectCode = $"{projectType.ProjectTypeCode}-ProjectCode{j}"
        });
    }
}






class GLProjectType
{ 
    public string ProjectTypeCode { get; set; }

    public string ProjectTypeName { get; set; }
}

class GLProject
{
    public string ProjectTypeCode { get; set; }

    public string ProjectName { get; set; }
    public string ProjectCode { get; set; }
}