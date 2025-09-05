using Elastic.Clients.Elasticsearch;
using ElasticSearchApi.Configuration;
using ElasticSearchApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElasticSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndexManagementController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticsearchSettings _settings;
    private readonly ILogger<IndexManagementController> _logger;

    public IndexManagementController(
        ElasticsearchClient client,
        IOptions<ElasticsearchSettings> settings,
        ILogger<IndexManagementController> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    /// <summary>
    /// 创建索引
    /// </summary>
    [HttpPost("create/{indexName}")]
    public async Task<IActionResult> CreateIndex(string indexName)
    {
        try
        {
            var response = await _client.Indices.CreateAsync(indexName, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                    .Analysis(a => a
                        .Analyzers(aa => aa
                            .Custom("my_ik_analyzer", ca => ca // 定义一个自定义分析器
                                .Tokenizer("ik_max_word")
                                .Filter("lowercase") // 可以添加令牌过滤器，如小写化
                            )
                        )
                    )
                )
                .Mappings(m => m
                    .Properties<Document>(p => p
                        .Keyword(k => k.Id)
                        .Text(t => t.Title, td => td.Analyzer("standard"))
                        .Text(t => t.Content, td => td.Analyzer("standard"))
                        .Keyword(k => k.Category)
                        .Keyword(k => k.Tags)
                        .Date(d => d.CreatedAt)
                        .Date(d => d.UpdatedAt)
                        .Keyword(k => k.Author)
                        .IntegerNumber(n => n.ViewCount)
                        .Boolean(b => b.IsPublished)
                    )
                )
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to create index {IndexName}: {Error}", indexName, response.DebugInformation);
                return BadRequest($"Failed to create index: {response.DebugInformation}");
            }

            return Ok(new { Message = $"Index '{indexName}' created successfully", Acknowledged = response.Acknowledged });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating index {IndexName}", indexName);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 删除索引
    /// </summary>
    [HttpDelete("delete/{indexName}")]
    public async Task<IActionResult> DeleteIndex(string indexName)
    {
        try
        {
            var response = await _client.Indices.DeleteAsync(indexName);

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to delete index {IndexName}: {Error}", indexName, response.DebugInformation);
                return BadRequest($"Failed to delete index: {response.DebugInformation}");
            }

            return Ok(new { Message = $"Index '{indexName}' deleted successfully", Acknowledged = response.Acknowledged });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting index {IndexName}", indexName);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 检查索引是否存在
    /// </summary>
    [HttpGet("exists/{indexName}")]
    public async Task<IActionResult> IndexExists(string indexName)
    {
        try
        {
            var response = await _client.Indices.ExistsAsync(indexName);

            return Ok(new { IndexName = indexName, Exists = response.Exists });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if index {IndexName} exists", indexName);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 获取索引信息
    /// </summary>
    [HttpGet("info/{indexName}")]
    public async Task<IActionResult> GetIndexInfo(string indexName)
    {
        try
        {
            var response = await _client.Indices.GetAsync(indexName);

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to get index info for {IndexName}: {Error}", indexName, response.DebugInformation);
                return BadRequest($"Failed to get index info: {response.DebugInformation}");
            }

            return Ok(response.Indices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting index info for {IndexName}", indexName);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 获取所有索引
    /// </summary>
    [HttpGet("list")]
    public async Task<IActionResult> ListIndices()
    {
        try
        {
            var response = await _client.Indices.GetAsync("*");

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to list indices: {Error}", response.DebugInformation);
                return BadRequest($"Failed to list indices: {response.DebugInformation}");
            }

            return Ok(response.Indices.Keys);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing indices");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 初始化默认索引
    /// </summary>
    [HttpPost("initialize")]
    public async Task<IActionResult> InitializeDefaultIndex()
    {
        try
        {
            // 检查默认索引是否存在
            var existsResponse = await _client.Indices.ExistsAsync(_settings.DefaultIndex);

            if (existsResponse.Exists)
            {
                return Ok(new { Message = $"Index '{_settings.DefaultIndex}' already exists" });
            }

            // 创建默认索引
            var createResponse = await _client.Indices.CreateAsync(_settings.DefaultIndex, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                    .Analysis(a => a
                        .Analyzers(analyzers => analyzers
                            .Standard("standard_analyzer")
                        )
                    )
                )
                .Mappings(m => m
                    .Properties<Document>(p => p
                        .Keyword(k => k.Id)
                        .Text(t => t.Title, td => td.Analyzer("standard_analyzer"))
                        .Text(t => t.Content, td => td.Analyzer("standard_analyzer"))
                        .Keyword(k => k.Category)
                        .Keyword(k => k.Tags)
                        .Date(d => d.CreatedAt)
                        .Date(d => d.UpdatedAt)
                        .Keyword(k => k.Author)
                        .IntegerNumber(n => n.ViewCount)
                        .Boolean(b => b.IsPublished)
                    )
                )
            );

            if (!createResponse.IsValidResponse)
            {
                _logger.LogError("Failed to initialize default index: {Error}", createResponse.DebugInformation);
                return BadRequest($"Failed to initialize default index: {createResponse.DebugInformation}");
            }

            return Ok(new
            {
                Message = $"Default index '{_settings.DefaultIndex}' initialized successfully",
                Acknowledged = createResponse.Acknowledged
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing default index");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 重建索引
    /// </summary>
    [HttpPost("reindex/{sourceIndex}/{targetIndex}")]
    public async Task<IActionResult> ReindexDocuments(string sourceIndex, string targetIndex)
    {
        try
        {
            var response = await _client.ReindexAsync(r => r
                .Source(s => s.Indices(sourceIndex))
                .Dest(d => d.Index(targetIndex))
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to reindex from {SourceIndex} to {TargetIndex}: {Error}",
                    sourceIndex, targetIndex, response.DebugInformation);
                return BadRequest($"Failed to reindex: {response.DebugInformation}");
            }

            return Ok(new
            {
                Message = $"Reindexing from '{sourceIndex}' to '{targetIndex}' completed",
                Total = response.Total,
                Created = response.Created,
                Updated = response.Updated,
                Deleted = response.Deleted
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reindexing from {SourceIndex} to {TargetIndex}", sourceIndex, targetIndex);
            return StatusCode(500, "Internal server error");
        }
    }
}