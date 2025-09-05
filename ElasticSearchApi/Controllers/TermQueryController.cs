using Elastic.Clients.Elasticsearch;
using ElasticSearchApi.Configuration;
using ElasticSearchApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElasticSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TermQueryController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticsearchSettings _settings;
    private readonly ILogger<TermQueryController> _logger;

    public TermQueryController(
        ElasticsearchClient client,
        IOptions<ElasticsearchSettings> settings,
        ILogger<TermQueryController> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    /// <summary>
    /// 精确匹配单个字段值
    /// </summary>
    [HttpGet("exact/{field}/{value}")]
    public async Task<IActionResult> TermQuery(string field, string value)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Term(t => t
                        .Field(field)
                        .Value(value)
                    )
                )
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Elasticsearch error: {Error}", response.DebugInformation);
                return BadRequest($"Search failed: {response.DebugInformation}");
            }

            return Ok(new
            {
                Total = response.Total,
                Documents = response.Documents
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing term query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 范围查询
    /// </summary>
    [HttpGet("range/{field}")]
    public async Task<IActionResult> RangeQuery(string field, [FromQuery] int? gte = null, [FromQuery] int? lte = null)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Range(r => r
                        .Number(nr => nr
                            .Field(field)
                            .Gte(gte)
                            .Lte(lte)
                        )
                    )
                )
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Elasticsearch error: {Error}", response.DebugInformation);
                return BadRequest($"Search failed: {response.DebugInformation}");
            }

            return Ok(new
            {
                Total = response.Total,
                Documents = response.Documents
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing range query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 布尔值精确匹配
    /// </summary>
    [HttpGet("bool/{field}/{value}")]
    public async Task<IActionResult> BoolTermQuery(string field, bool value)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Term(t => t
                        .Field(field)
                        .Value(value)
                    )
                )
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Elasticsearch error: {Error}", response.DebugInformation);
                return BadRequest($"Search failed: {response.DebugInformation}");
            }

            return Ok(new
            {
                Total = response.Total,
                Documents = response.Documents
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing bool term query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 存在性查询
    /// </summary>
    [HttpGet("exists/{field}")]
    public async Task<IActionResult> ExistsQuery(string field)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Exists(e => e
                        .Field(field)
                    )
                )
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Elasticsearch error: {Error}", response.DebugInformation);
                return BadRequest($"Search failed: {response.DebugInformation}");
            }

            return Ok(new
            {
                Total = response.Total,
                Documents = response.Documents
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing exists query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 前缀查询
    /// </summary>
    [HttpGet("prefix/{field}/{prefix}")]
    public async Task<IActionResult> PrefixQuery(string field, string prefix)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Prefix(p => p
                        .Field(field)
                        .Value(prefix)
                    )
                )
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Elasticsearch error: {Error}", response.DebugInformation);
                return BadRequest($"Search failed: {response.DebugInformation}");
            }

            return Ok(new
            {
                Total = response.Total,
                Documents = response.Documents
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing prefix query");
            return StatusCode(500, "Internal server error");
        }
    }
}

public class TermsQueryRequest
{
    public string Field { get; set; } = string.Empty;
    public List<string> Values { get; set; } = new();
}