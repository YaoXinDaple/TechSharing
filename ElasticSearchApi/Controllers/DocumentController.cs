using Elastic.Clients.Elasticsearch;
using ElasticSearchApi.Configuration;
using ElasticSearchApi.Models;
using ElasticSearchApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElasticSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticsearchSettings _settings;
    private readonly ILogger<DocumentController> _logger;

    public DocumentController(
        ElasticsearchClient client,
        IOptions<ElasticsearchSettings> settings,
        ILogger<DocumentController> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    /// <summary>
    /// 创建文档
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentRequest request)
    {
        try
        {
            var document = new Document
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Content = request.Content,
                Category = request.Category,
                Tags = request.Tags,
                Author = request.Author,
                IsPublished = request.IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await _client.IndexAsync(document, _settings.DefaultIndex, document.Id);

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to create document: {Error}", response.DebugInformation);
                return BadRequest($"Failed to create document: {response.DebugInformation}");
            }

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating document");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 获取单个文档
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocument(string id)
    {
        try
        {
            var response = await _client.GetAsync<Document>(id, idx => idx.Index(_settings.DefaultIndex));

            if (!response.IsValidResponse || !response.Found)
            {
                return NotFound($"Document with ID {id} not found");
            }

            return Ok(response.Source);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving document {DocumentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 更新文档
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDocument(string id, [FromBody] UpdateDocumentRequest request)
    {
        try
        {
            // 首先检查文档是否存在
            var getResponse = await _client.GetAsync<Document>(id, idx => idx.Index(_settings.DefaultIndex));
            if (!getResponse.IsValidResponse || !getResponse.Found)
            {
                return NotFound($"Document with ID {id} not found");
            }

            var document = getResponse.Source!;

            // 更新字段
            if (!string.IsNullOrEmpty(request.Title))
                document.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Content))
                document.Content = request.Content;
            if (!string.IsNullOrEmpty(request.Category))
                document.Category = request.Category;
            if (request.Tags != null)
                document.Tags = request.Tags;
            if (!string.IsNullOrEmpty(request.Author))
                document.Author = request.Author;
            if (request.IsPublished.HasValue)
                document.IsPublished = request.IsPublished.Value;
            
            document.UpdatedAt = DateTime.UtcNow;

            var updateResponse = await _client.IndexAsync(document, _settings.DefaultIndex, id);

            if (!updateResponse.IsValidResponse)
            {
                _logger.LogError("Failed to update document: {Error}", updateResponse.DebugInformation);
                return BadRequest($"Failed to update document: {updateResponse.DebugInformation}");
            }

            return Ok(document);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating document {DocumentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 部分更新文档
    /// </summary>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PartialUpdateDocument(string id, [FromBody] UpdateDocumentRequest request)
    {
        try
        {
            var updateDoc = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(request.Title))
                updateDoc["title"] = request.Title;
            if (!string.IsNullOrEmpty(request.Content))
                updateDoc["content"] = request.Content;
            if (!string.IsNullOrEmpty(request.Category))
                updateDoc["category"] = request.Category;
            if (request.Tags != null)
                updateDoc["tags"] = request.Tags;
            if (!string.IsNullOrEmpty(request.Author))
                updateDoc["author"] = request.Author;
            if (request.IsPublished.HasValue)
                updateDoc["isPublished"] = request.IsPublished.Value;
            
            updateDoc["updatedAt"] = DateTime.UtcNow;

            var response = await _client.UpdateAsync<Document, object>(_settings.DefaultIndex, id, u => u
                .Doc(updateDoc)
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to partially update document: {Error}", response.DebugInformation);
                return BadRequest($"Failed to update document: {response.DebugInformation}");
            }

            // 获取更新后的文档
            var getResponse = await _client.GetAsync<Document>(id, idx => idx.Index(_settings.DefaultIndex));
            return Ok(getResponse.Source);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error partially updating document {DocumentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 删除文档
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(string id)
    {
        try
        {
            var response = await _client.DeleteAsync(_settings.DefaultIndex, id);

            if (!response.IsValidResponse)
            {
                if (response.Result == Result.NotFound)
                {
                    return NotFound($"Document with ID {id} not found");
                }
                
                _logger.LogError("Failed to delete document: {Error}", response.DebugInformation);
                return BadRequest($"Failed to delete document: {response.DebugInformation}");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting document {DocumentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// 获取所有文档（分页）
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllDocuments([FromQuery] int from = 0, [FromQuery] int size = 10)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .From(from)
                .Size(size)
                .Query(q => q.MatchAll())
                .Sort(sort => sort.Field("createdAt", SortOrder.Desc))
            );

            if (!response.IsValidResponse)
            {
                _logger.LogError("Failed to retrieve documents: {Error}", response.DebugInformation);
                return BadRequest($"Failed to retrieve documents: {response.DebugInformation}");
            }

            return Ok(new
            {
                Total = response.Total,
                From = from,
                Size = size,
                Documents = response.Documents
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving documents");
            return StatusCode(500, "Internal server error");
        }
    }
}