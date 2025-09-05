using Elastic.Clients.Elasticsearch;
using ElasticSearchApi.Configuration;
using ElasticSearchApi.Models;
using ElasticSearchApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElasticSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BulkOperationsController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticsearchSettings _settings;
    private readonly ILogger<BulkOperationsController> _logger;

    public BulkOperationsController(
        ElasticsearchClient client,
        IOptions<ElasticsearchSettings> settings,
        ILogger<BulkOperationsController> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    /// <summary>
    /// 批量创建文档
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> BulkCreateDocuments([FromBody] BulkDocumentRequest request)
    {
        try
        {
            var documents = request.Documents.Select(doc => new Document
            {
                Id = Guid.NewGuid().ToString(),
                Title = doc.Title,
                Content = doc.Content,
                Category = doc.Category,
                Tags = doc.Tags,
                Author = doc.Author,
                IsPublished = doc.IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }).ToList();

            var response = await _client.BulkAsync(b => b
                .Index(_settings.DefaultIndex)
                .IndexMany(documents, (desc, doc) => desc.Id(doc.Id))
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Bulk create failed: {Error}", response.DebugInformation);
                return BadRequest($"Bulk create failed: {response.DebugInformation}");
            }

            var results = new
            {
                TotalItems = response.Items.Count,
                SuccessfulItems = response.Items.Count(i => i.IsValid),
                FailedItems = response.Items.Count(i => !i.IsValid),
                Errors = response.Items.Where(i => !i.IsValid).Select(i => new
                {
                    Id = i.Id,
                    Error = i.Error?.Reason
                }),
                CreatedDocuments = documents
            };

            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in bulk create operation");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 批量更新文档 - 使用循环方式
    /// </summary>
    [HttpPut("update")]
    public async Task<IActionResult> BulkUpdateDocuments([FromBody] BulkUpdateRequest request)
    {
        try
        {
            var operations = new List<object>();

            foreach (var update in request.Updates)
            {
                // 创建更新操作的元数据
                operations.Add(new { update = new { _index = _settings.DefaultIndex, _id = update.Id } });
                
                // 创建要更新的文档数据
                operations.Add(new { 
                    doc = new {
                        title = update.Title,
                        content = update.Content,
                        category = update.Category,
                        tags = update.Tags,
                        author = update.Author,
                        isPublished = update.IsPublished,
                        updatedAt = DateTime.UtcNow
                    }
                });
            }

            // 手动执行批量更新
            var updateTasks = request.Updates.Select(async update =>
            {
                try
                {
                    var updateDoc = new Dictionary<string, object>();
                    if (!string.IsNullOrEmpty(update.Title)) updateDoc["title"] = update.Title;
                    if (!string.IsNullOrEmpty(update.Content)) updateDoc["content"] = update.Content;
                    if (!string.IsNullOrEmpty(update.Category)) updateDoc["category"] = update.Category;
                    if (update.Tags != null) updateDoc["tags"] = update.Tags;
                    if (!string.IsNullOrEmpty(update.Author)) updateDoc["author"] = update.Author;
                    if (update.IsPublished.HasValue) updateDoc["isPublished"] = update.IsPublished.Value;
                    updateDoc["updatedAt"] = DateTime.UtcNow;

                    return await _client.UpdateAsync<Document, object>(_settings.DefaultIndex, update.Id, u => u
                        .Doc(updateDoc)
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating document {Id}", update.Id);
                    return null;
                }
            });

            var updateResults = await Task.WhenAll(updateTasks);
            var successCount = updateResults.Count(r => r?.IsValidResponse == true);
            var failCount = updateResults.Length - successCount;

            return Ok(new
            {
                TotalItems = updateResults.Length,
                SuccessfulItems = successCount,
                FailedItems = failCount,
                Message = $"Updated {successCount} documents, {failCount} failed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in bulk update operation");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 批量删除文档
    /// </summary>
    [HttpDelete("delete")]
    public async Task<IActionResult> BulkDeleteDocuments([FromBody] BulkDeleteRequest request)
    {
        try
        {
            var deleteTasks = request.Ids.Select(async id =>
            {
                try
                {
                    return await _client.DeleteAsync(_settings.DefaultIndex, id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting document {Id}", id);
                    return null;
                }
            });

            var deleteResults = await Task.WhenAll(deleteTasks);
            var successCount = deleteResults.Count(r => r?.IsValidResponse == true);
            var failCount = deleteResults.Length - successCount;

            return Ok(new
            {
                TotalItems = deleteResults.Length,
                SuccessfulItems = successCount,
                FailedItems = failCount,
                Message = $"Deleted {successCount} documents, {failCount} failed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in bulk delete operation");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 简单的批量索引操作
    /// </summary>
    [HttpPost("simple-bulk")]
    public async Task<IActionResult> SimpleBulkOperation([FromBody] SimpleBulkRequest request)
    {
        try
        {
            var results = new List<object>();

            // 处理创建操作
            if (request.Create != null && request.Create.Any())
            {
                foreach (var createDoc in request.Create)
                {
                    var document = new Document
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = createDoc.Title,
                        Content = createDoc.Content,
                        Category = createDoc.Category,
                        Tags = createDoc.Tags,
                        Author = createDoc.Author,
                        IsPublished = createDoc.IsPublished,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    var createResponse = await _client.IndexAsync(document, _settings.DefaultIndex, document.Id);
                    results.Add(new { Operation = "create", Id = document.Id, Success = createResponse.IsValidResponse });
                }
            }

            // 处理删除操作
            if (request.Delete != null && request.Delete.Any())
            {
                foreach (var deleteId in request.Delete)
                {
                    var deleteResponse = await _client.DeleteAsync(_settings.DefaultIndex, deleteId);
                    results.Add(new { Operation = "delete", Id = deleteId, Success = deleteResponse.IsValidResponse });
                }
            }

            var successCount = results.Count(r => (bool)((dynamic)r).Success);
            var failCount = results.Count - successCount;

            return Ok(new
            {
                TotalOperations = results.Count,
                SuccessfulOperations = successCount,
                FailedOperations = failCount,
                Details = results
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in simple bulk operation");
            return StatusCode(500, "Internal server error");
        }
    }
}

public class BulkUpdateRequest
{
    public List<BulkUpdateItem> Updates { get; set; } = new();
}

public class BulkUpdateItem
{
    public string Id { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Category { get; set; }
    public List<string>? Tags { get; set; }
    public string? Author { get; set; }
    public bool? IsPublished { get; set; }
}

public class BulkDeleteRequest
{
    public List<string> Ids { get; set; } = new();
}

public class SimpleBulkRequest
{
    public List<CreateDocumentRequest>? Create { get; set; }
    public List<string>? Delete { get; set; }
}