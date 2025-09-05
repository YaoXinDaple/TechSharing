using ElasticSearchApi.Configuration;
using ElasticSearchApi.Models;
using ElasticSearchApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElasticSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemoController : ControllerBase
{
    private readonly ElasticsearchSettings _settings;
    private readonly ILogger<DemoController> _logger;

    public DemoController(
        IOptions<ElasticsearchSettings> settings,
        ILogger<DemoController> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    /// <summary>
    /// 获取 Elasticsearch 配置信息
    /// </summary>
    [HttpGet("config")]
    public IActionResult GetConfiguration()
    {
        return Ok(new
        {
            ElasticsearchUri = _settings.Uri,
            DefaultIndex = _settings.DefaultIndex,
            Message = "Elasticsearch API is ready to use!"
        });
    }

    /// <summary>
    /// 创建示例文档数据
    /// </summary>
    [HttpGet("sample-data")]
    public IActionResult GetSampleData()
    {
        var sampleDocuments = new List<CreateDocumentRequest>
        {
            new CreateDocumentRequest
            {
                Title = "Elasticsearch 入门指南",
                Content = "这是一篇关于 Elasticsearch 基础知识的文章，介绍如何使用 Elasticsearch 进行搜索和分析。",
                Category = "技术文档",
                Tags = new List<string> { "elasticsearch", "搜索引擎", "全文搜索" },
                Author = "技术团队",
                IsPublished = true
            },
            new CreateDocumentRequest
            {
                Title = ".NET Core 微服务架构",
                Content = "详细介绍了如何使用 .NET Core 构建微服务架构，包括服务发现、负载均衡等。",
                Category = "架构设计",
                Tags = new List<string> { "dotnet", "微服务", "架构" },
                Author = "架构师",
                IsPublished = true
            },
            new CreateDocumentRequest
            {
                Title = "Docker 容器化部署",
                Content = "学习如何使用 Docker 进行应用程序的容器化部署，提高开发和运维效率。",
                Category = "运维部署",
                Tags = new List<string> { "docker", "容器", "部署" },
                Author = "运维工程师",
                IsPublished = false
            }
        };

        return Ok(new
        {
            Message = "Sample documents for testing",
            Count = sampleDocuments.Count,
            Documents = sampleDocuments
        });
    }

    /// <summary>
    /// API 使用指南
    /// </summary>
    [HttpGet("usage-guide")]
    public IActionResult GetUsageGuide()
    {
        var guide = new
        {
            Message = "ElasticSearch API 使用指南",
            Steps = new[]
            {
                "1. 首先启动 docker-compose up -d 启动 Elasticsearch 和 Kibana",
                "2. 调用 POST /api/IndexManagement/initialize 初始化默认索引",
                "3. 使用 POST /api/Document 创建文档",
                "4. 使用各种查询 API 进行搜索测试",
                "5. 访问 http://localhost:5601 使用 Kibana 查看数据"
            },
            AvailableControllers = new[]
            {
                "DocumentController - 文档 CRUD 操作",
                "TermQueryController - 精确匹配查询",
                "MatchQueryController - 全文搜索查询", 
                "BulkOperationsController - 批量操作",
                "IndexManagementController - 索引管理"
            },
            ExampleRequests = new
            {
                CreateDocument = "POST /api/Document",
                SearchByTitle = "GET /api/MatchQuery/single/title/关键词",
                ExactMatch = "GET /api/TermQuery/exact/category/技术文档",
                GetAllDocuments = "GET /api/Document?from=0&size=10"
            }
        };

        return Ok(guide);
    }

    /// <summary>
    /// 健康检查
    /// </summary>
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        });
    }
}