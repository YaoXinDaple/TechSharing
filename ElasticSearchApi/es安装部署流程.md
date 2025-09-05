## ElasticSearch 集成 Kibana 安装及部署流程


### 通过yml文件创建
```
docker-compose up -d
```

### 手动逐步创建

#### 创建本地网络
```
docker network create elastic
```

### 拉取并安装 Elastic 镜像
```
docker run -d --name es01 --network elastic  -p 9200:9200  -p 9300:9300 -e "discovery.type=single-node"  -e "ES_JAVA_OPTS=-Xms1g -Xmx1g"   -e "xpack.security.enabled=false"  -v es_data:/usr/share/elasticsearch/data docker.elastic.co/elasticsearch/elasticsearch:latest

# 参数说明
--name : 容器名称
--network : 使用的网络
-p : 端口映射
-e : discovery.type:服务发现类型（single-node:单节点）,ES_JAVA_OPTS: 堆内存限制，xpack.security.enabled：安全登录限制
-v : es_data:文件卷路径
docker.elastic.co/elasticsearch/elasticsearch:latest ： ElasticSearch 镜像仓库地址，也可以手动拉取后，直接通过 docker images命令找到ElasticSearch 的 ImageID，输入该ImageID
```

### 拉取并安装 Kibana 镜像
```bash
docker run -d   --name kibana   --network elastic  -p 5601:5601 -e "I18N_LOCALE=zh-CN" -e ELASTICSEARCH_HOSTS=http://es01:9200   docker.elastic.co/kibana/kibana:latest

# 参数说明
--name : 容器名称
--network : 使用的网络
-p : 端口映射
-e : ELASTICSEARCH_HOSTS:es服务地址,I18N_LOCALE:环境变量->中文
docker.elastic.co/kibana/kibana:latest : kibana远程镜像仓库地址，也可以输入kibana本地ImageID
```

### 注意点
es和kibana要使用同一网络
两者版本号要一一对应
分词器版本号也必须和es版本号严格对应

安装 中文分词器 IK分词器

在 https://release.infinilabs.com/analysis-ik/stable/ 找到对应版本的安装包下载地址

在es容器中 Exec tab页执行一下命令安装
```
bin/elasticsearch-plugin install https://release.infinilabs.com/analysis-ik/stable/elasticsearch-analysis-ik-9.1.3.zip
```
