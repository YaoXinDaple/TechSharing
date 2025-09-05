namespace ElasticSearchApi.Models.Requests;

public class CreateDocumentRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string Author { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = true;
}

public class UpdateDocumentRequest
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Category { get; set; }
    public List<string>? Tags { get; set; }
    public string? Author { get; set; }
    public bool? IsPublished { get; set; }
}

public class BulkDocumentRequest
{
    public List<CreateDocumentRequest> Documents { get; set; } = new();
}

public class SearchRequest
{
    public string Query { get; set; } = string.Empty;
    public int From { get; set; } = 0;
    public int Size { get; set; } = 10;
    public string? Category { get; set; }
    public List<string>? Tags { get; set; }
}