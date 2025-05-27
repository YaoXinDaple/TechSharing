using BenchmarkDotNet.Attributes;
using ZLinq;

namespace ZlinqCompareWithLinq
{
    [MemoryDiagnoser]
    public class LinqAllocateBenchmark
    {
        private List<GLProjectType> projectTypes = new List<GLProjectType>();
        private List<GLProject> projects = new List<GLProject>();
        private Dictionary<(string, string), GLProject> projectTypeCodeAndNameValueTupleKeyDict = new Dictionary<(string, string), GLProject>();
        private Dictionary<string, GLProject> projectTypeCodeAndNameStringKeyDict = new Dictionary<string, GLProject>();
        [GlobalSetup]
        public void Setup()
        {
            //创建10个GLProjectType 实例
            for (int i = 0; i < 10; i++)
            {
                projectTypes.Add(new GLProjectType
                {
                    ProjectTypeCode = $"Code{i}",
                    ProjectTypeName = $"Name{i}"
                });
            }
            //为 projectTypes 中每个实例生成 10-50个 GLProject 实例
            for (int i = 0; i < projectTypes.Count; i++)
            {
                var projectType = projectTypes[i];
                var projectCount = new Random().Next(10, 500);
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
        }

        [Benchmark]
        public void WhereThenCount()
        {
            var count = projects.Where(p => p.ProjectTypeCode == "Code0").Count();
        }

        [Benchmark]
        public void StrictCount()
        {
            var count = projects.Count(p => p.ProjectTypeCode == "Code0");
        }

        [Benchmark]
        public void AsValueEnumerableWhereThenCount()
        {
            var count = projects.AsValueEnumerable().Where(p => p.ProjectTypeCode == "Code0").Count();
        }



        //[Benchmark]
        //public void LinqUseString()
        //{
        //    projects.Select(p => projectTypeCodeAndNameStringKeyDict.TryAdd(p.ProjectTypeCode + p.ProjectName, p)).ToList();
        //}
        //[Benchmark]
        //public void LinqUseValueTuple()
        //{
        //    projects.Select(p => projectTypeCodeAndNameValueTupleKeyDict.TryAdd((p.ProjectTypeCode, p.ProjectName), p)).ToList();
        //}

        //[Benchmark]
        //public void UseManualLoopString()
        //{
        //    for (int i = 0; i < projects.Count; i++)
        //    {
        //        var p = projects[i];
        //        projectTypeCodeAndNameStringKeyDict.TryAdd(p.ProjectTypeCode + p.ProjectName, p);
        //    }
        //}

        //[Benchmark]
        //public void UseManualLoopValueTuple()
        //{
        //    for (int i = 0; i < projects.Count; i++)
        //    {
        //        var p = projects[i];
        //        projectTypeCodeAndNameValueTupleKeyDict.TryAdd((p.ProjectTypeCode, p.ProjectName), p);
        //    }
        //}

    }
}
