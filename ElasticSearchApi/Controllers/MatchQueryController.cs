using Elastic.Clients.Elasticsearch;
using ElasticSearchApi.Configuration;
using ElasticSearchApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ElasticSearchApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchQueryController : ControllerBase
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticsearchSettings _settings;
    private readonly ILogger<MatchQueryController> _logger;

    public MatchQueryController(
        ElasticsearchClient client,
        IOptions<ElasticsearchSettings> settings,
        ILogger<MatchQueryController> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    /// <summary>
    /// È«ÎÄËÑË÷µ¥¸ö×Ö¶Î
    /// </summary>
    [HttpGet("single/{field}/{query}")]
    public async Task<IActionResult> MatchQuery(string field, string query)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Match(m => m
                        .Field(field)
                        .Query(query)
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
            _logger.LogError(ex, "Error executing match query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// ¶à×Ö¶ÎÈ«ÎÄËÑË÷
    /// </summary>
    [HttpPost("multi-field")]
    public async Task<IActionResult> MultiMatchQuery([FromBody] MultiMatchRequest request)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query(request.Query)
                        .Fields(request.Fields.ToArray())
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
            _logger.LogError(ex, "Error executing multi-match query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// ¶ÌÓïÆ¥Åä
    /// </summary>
    [HttpGet("phrase/{field}/{phrase}")]
    public async Task<IActionResult> MatchPhraseQuery(string field, string phrase)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .MatchPhrase(mp => mp
                        .Field(field)
                        .Query(phrase)
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
            _logger.LogError(ex, "Error executing match phrase query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Ä£ºýÆ¥Åä
    /// </summary>
    [HttpGet("fuzzy/{field}/{query}")]
    public async Task<IActionResult> FuzzyQuery(string field, string query, [FromQuery] int fuzziness = 2)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Fuzzy(f => f
                        .Field(field)
                        .Value(query)
                        .Fuzziness(new Fuzziness(fuzziness))
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
            _logger.LogError(ex, "Error executing fuzzy query");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Í¨Åä·û²éÑ¯
    /// </summary>
    [HttpGet("wildcard/{field}/{pattern}")]
    public async Task<IActionResult> WildcardQuery(string field, string pattern)
    {
        try
        {
            var response = await _client.SearchAsync<Document>(s => s
                .Indices(_settings.DefaultIndex)
                .Query(q => q
                    .Wildcard(w => w
                        .Field(field)
                        .Value(pattern)
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
            _logger.LogError(ex, "Error executing wildcard query");
            return StatusCode(500, "Internal server error");
        }
    }
}

public class MultiMatchRequest
{
    public string Query { get; set; } = string.Empty;
    public List<string> Fields { get; set; } = new();
}