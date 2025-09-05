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
    /// ��ȡ Elasticsearch ������Ϣ
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
    /// ����ʾ���ĵ�����
    /// </summary>
    [HttpGet("sample-data")]
    public IActionResult GetSampleData()
    {
        var sampleDocuments = new List<CreateDocumentRequest>
        {
            new CreateDocumentRequest
            {
                Title = "Elasticsearch ����ָ��",
                Content = "����һƪ���� Elasticsearch ����֪ʶ�����£��������ʹ�� Elasticsearch ���������ͷ�����",
                Category = "�����ĵ�",
                Tags = new List<string> { "elasticsearch", "��������", "ȫ������" },
                Author = "�����Ŷ�",
                IsPublished = true
            },
            new CreateDocumentRequest
            {
                Title = ".NET Core ΢����ܹ�",
                Content = "��ϸ���������ʹ�� .NET Core ����΢����ܹ������������֡����ؾ���ȡ�",
                Category = "�ܹ����",
                Tags = new List<string> { "dotnet", "΢����", "�ܹ�" },
                Author = "�ܹ�ʦ",
                IsPublished = true
            },
            new CreateDocumentRequest
            {
                Title = "Docker ����������",
                Content = "ѧϰ���ʹ�� Docker ����Ӧ�ó����������������߿�������άЧ�ʡ�",
                Category = "��ά����",
                Tags = new List<string> { "docker", "����", "����" },
                Author = "��ά����ʦ",
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
    /// API ʹ��ָ��
    /// </summary>
    [HttpGet("usage-guide")]
    public IActionResult GetUsageGuide()
    {
        var guide = new
        {
            Message = "ElasticSearch API ʹ��ָ��",
            Steps = new[]
            {
                "1. �������� docker-compose up -d ���� Elasticsearch �� Kibana",
                "2. ���� POST /api/IndexManagement/initialize ��ʼ��Ĭ������",
                "3. ʹ�� POST /api/Document �����ĵ�",
                "4. ʹ�ø��ֲ�ѯ API ������������",
                "5. ���� http://localhost:5601 ʹ�� Kibana �鿴����"
            },
            AvailableControllers = new[]
            {
                "DocumentController - �ĵ� CRUD ����",
                "TermQueryController - ��ȷƥ���ѯ",
                "MatchQueryController - ȫ��������ѯ", 
                "BulkOperationsController - ��������",
                "IndexManagementController - ��������"
            },
            ExampleRequests = new
            {
                CreateDocument = "POST /api/Document",
                SearchByTitle = "GET /api/MatchQuery/single/title/�ؼ���",
                ExactMatch = "GET /api/TermQuery/exact/category/�����ĵ�",
                GetAllDocuments = "GET /api/Document?from=0&size=10"
            }
        };

        return Ok(guide);
    }

    /// <summary>
    /// �������
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