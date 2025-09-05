# ElasticSearch API

这是一个基于 .NET 9 和 Elastic.Clients.Elasticsearch 9.1.3 的完整 Elasticsearch 操作演示项目。

## 🚀 功能特性

本项目提供了完整的 Elasticsearch 操作功能，包括：

- **文档管理**: 创建、读取、更新、删除文档
- **Term 查询**: 精确匹配查询、范围查询、存在性查询
- **Match 查询**: 全文搜索、多字段搜索、短语匹配、模糊搜索
- **批量操作**: 批量创建、更新、删除文档
- **索引管理**: 索引的创建、删除、重建等操作
- **演示功能**: 配置信息、示例数据、使用指南

## 📋 技术栈

- **.NET 9**: 最新的 .NET 版本
- **Elastic.Clients.Elasticsearch 9.1.3**: 官方 Elasticsearch 客户端
- **ASP.NET Core**: Web API 框架
- **Docker Compose**: 用于运行 Elasticsearch 和 Kibana

## 🛠️ 快速开始

### 1. 启动 Elasticsearch 和 Kibana

```bash
cd ElasticSearchApi
docker-compose up -d
```

这将启动：
- Elasticsearch: http://localhost:9200
- Kibana: http://localhost:5601

### 2. 运行应用

```bash
dotnet run
```

应用将在 https://localhost:7xxx 启动

### 3. 初始化索引

首次使用前，建议初始化默认索引：

```http
POST /api/IndexManagement/initialize
```

### 4. 查看配置和使用指南

```http
GET /api/Demo/config
GET /api/Demo/usage-guide
GET /api/Demo/sample-data
```

## 📚 API 接口文档

### 🗂️ 演示功能 (Demo)

- `GET /api/Demo/config` - 获取配置信息
- `GET /api/Demo/usage-guide` - 获取使用指南
- `GET /api/Demo/sample-data` - 获取示例数据
- `GET /api/Demo/health` - 健康检查

### 🗃️ 索引管理 (IndexManagement)

- `POST /api/IndexManagement/initialize` - 初始化默认索引
- `POST /api/IndexManagement/create/{indexName}` - 创建索引
- `DELETE /api/IndexManagement/delete/{indexName}` - 删除索引
- `GET /api/IndexManagement/exists/{indexName}` - 检查索引是否存在
- `GET /api/IndexManagement/info/{indexName}` - 获取索引信息
- `GET /api/IndexManagement/list` - 获取所有索引

### 📄 文档操作 (Document)

- `POST /api/Document` - 创建文档
- `GET /api/Document/{id}` - 获取文档
- `PUT /api/Document/{id}` - 更新文档
- `PATCH /api/Document/{id}` - 部分更新文档
- `DELETE /api/Document/{id}` - 删除文档
- `GET /api/Document` - 获取所有文档（分页）

### 🎯 Term 查询 (TermQuery)

- `GET /api/TermQuery/exact/{field}/{value}` - 精确匹配单个字段
- `GET /api/TermQuery/range/{field}?gte=&lte=` - 范围查询
- `GET /api/TermQuery/bool/{field}/{value}` - 布尔值精确匹配
- `GET /api/TermQuery/exists/{field}` - 存在性查询
- `GET /api/TermQuery/prefix/{field}/{prefix}` - 前缀查询

### 🔍 Match 查询 (MatchQuery)

- `GET /api/MatchQuery/single/{field}/{query}` - 单字段全文搜索
- `POST /api/MatchQuery/multi-field` - 多字段全文搜索
- `GET /api/MatchQuery/phrase/{field}/{phrase}` - 短语匹配
- `GET /api/MatchQuery/fuzzy/{field}/{query}` - 模糊匹配
- `GET /api/MatchQuery/wildcard/{field}/{pattern}` - 通配符查询

### 📦 批量操作 (BulkOperations)

- `POST /api/BulkOperations/create` - 批量创建文档
- `PUT /api/BulkOperations/update` - 批量更新文档
- `DELETE /api/BulkOperations/delete` - 批量删除文档
- `POST /api/BulkOperations/simple-bulk` - 简单批量操作

## 📊 数据模型

### Document 模型

```json
{
  "id": "string",
  "title": "string",
  "content": "string",
  "category": "string",
  "tags": ["string"],
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z",
  "author": "string",
  "viewCount": 0,
  "isPublished": true
}
```

## 🔧 使用示例

### 1. 获取配置信息

```bash
curl -X GET "https://localhost:7xxx/api/Demo/config"
```

### 2. 创建文档

```bash
curl -X POST "https://localhost:7xxx/api/Document" \
-H "Content-Type: application/json" \
-d '{
  "title": "示例文档",
  "content": "这是一个示例文档的内容",
  "category": "技术",
  "tags": ["elasticsearch", "dotnet"],
  "author": "开发者",
  "isPublished": true
}'
```

### 3. 全文搜索

```bash
curl -X GET "https://localhost:7xxx/api/MatchQuery/single/title/示例"
```

### 4. 精确匹配

```bash
curl -X GET "https://localhost:7xxx/api/TermQuery/exact/category/技术"
```

### 5. 批量创建文档

```bash
curl -X POST "https://localhost:7xxx/api/BulkOperations/create" \
-H "Content-Type: application/json" \
-d '{
  "documents": [
    {
      "title": "文档1",
      "content": "内容1",
      "category": "技术",
      "tags": ["tag1"],
      "author": "作者1"
    },
    {
      "title": "文档2", 
      "content": "内容2",
      "category": "教程",
      "tags": ["tag2"],
      "author": "作者2"
    }
  ]
}'
```

## ⚙️ 配置说明

### appsettings.json

```json
{
  "Elasticsearch": {
    "Uri": "http://localhost:9200",
    "DefaultIndex": "documents"
  }
}
```

### Docker Compose 配置

- Elasticsearch 端口: 9200
- Kibana 端口: 5601
- 单节点模式
- 禁用安全认证（仅用于开发环境）

## 🚨 注意事项

1. 本项目仅用于开发和学习目的
2. 生产环境中应启用 Elasticsearch 的安全功能
3. 建议根据实际需求调整索引映射和设置
4. 批量操作时注意数据量，避免内存溢出

## 🔄 测试流程

1. **启动服务**: `docker-compose up -d`
2. **运行应用**: `dotnet run`
3. **初始化索引**: `POST /api/IndexManagement/initialize`
4. **创建示例数据**: 使用 `/api/Demo/sample-data` 获取示例数据，然后创建文档
5. **测试搜索**: 使用各种查询 API 测试搜索功能
6. **查看结果**: 在 Kibana (http://localhost:5601) 中查看数据

## 🚀 扩展功能

项目结构支持轻松扩展更多功能：

- 聚合查询 (Aggregations)
- 复杂的布尔查询 (Complex Bool Queries)
- 自定义分析器 (Custom Analyzers)
- 索引模板 (Index Templates)
- 管道聚合 (Pipeline Aggregations)
- 地理位置查询 (Geo Queries)

## 📖 技术支持

如有问题，请参考：
- [Elasticsearch 官方文档](https://www.elastic.co/guide/en/elasticsearch/reference/current/index.html)
- [Elastic.Clients.Elasticsearch 文档](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html)
- [Docker Compose 文档](https://docs.docker.com/compose/)

## 🤝 贡献

欢迎提交 Issue 和 Pull Request 来改进这个项目！

## 📄 许可证

MIT License
