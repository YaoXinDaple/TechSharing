// See https://aka.ms/new-console-template for more information
using Nullable.Research;

Console.WriteLine("Hello, World!");



var result1 = new Result(0.7m, 0.8m); var result2 = new Result(0.8m, 0.9m);
var
experiment1 = new Experiment("Experiment 1", result1);
var experiment2 = new Experiment("Experiment 2", result2);

var experimentalPhase = new ExperimentalPhase("Phase 1", experiment2, [experiment1, experiment2]);

var project = new ResearchProject("Project Alpha", experimentalPhase);

decimal score = project.ExperimentalPhase?.LatestExperiment?.Result?.ConfidenceScore ?? 0;

decimal scores = project
    .ExperimentalPhase?
    .HistoricalExperiments
    .Where(e=>e is not null)?
    .Sum(e => e.Result?.ConfidenceScore ?? 0) ?? 0;


var analyzer = new ResearchAnalyzer();
var report = analyzer.GenerateResearchReport(project);


if (report != null)
{
    Console.WriteLine($"Project Name: {report.ProjectName}");
    Console.WriteLine($"stages:{string.Join("->", report.Stages)}");
    Console.WriteLine($"Experiment Count: {report.ExperimentCount}");
    Console.WriteLine($"Latest Confidence Score: {report.LatestConfidenceScore?.ToString() ?? "N/A"}");
    Console.WriteLine($"Trend: {report.Trend?.TrendDirection.ToString() ?? "N/A"}");
    Console.WriteLine($"Most Successful Experiment: {report.MostSuccessfulExperiment ?? "N/A"}");
    Console.WriteLine($"Overall status :{report.OverallStatus}");

}
else
{
    Console.WriteLine("Unable to generate report.");
}