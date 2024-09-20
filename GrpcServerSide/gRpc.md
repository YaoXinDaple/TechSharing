## gRpc

### 1. What is gRpc?
- gRPC is a high performance, open-source universal RPC framework
- gRPC was developed by Google and is based on the HTTP/2 protocol
- gRPC uses Protocol Buffers as the interface definition language


### 2. What is Protocol Buffers?
- Protocol Buffers is a method developed by Google to serialize structured data
- Protocol Buffers is used to serialize structured data into a binary format
- Protocol Buffers is used to serialize structured data into a text format


### 3. What is the difference between gRPC and REST?
- gRPC is a high performance, open-source universal RPC framework
- REST is an architectural style for building distributed systems
- gRPC is based on the HTTP/2 protocol
- REST is based on the HTTP/1.1 protocol
- gRPC uses Protocol Buffers as the interface definition language
- REST uses JSON or XML as the interface definition language
- gRPC is designed to be efficient, fast, and language-independent
- REST is designed to be simple, flexible, and language-independent

## HTTP/2
	说到Http/2，我们首先要了解一下Http/1.1的问题，Http/1.1的问题主要有：
	1. 串行的请求，导致请求阻塞
	2. 请求头冗余，导致传输效率低

串行请求的源头在于Http协议是基于TCP协议的，而TCP协议是一个面向连接的协议，所以在一个连接上只能发送一个请求，这就导致了串行请求。

	Http/1.1相比Http/1.0的改进：
	1. 支持长连接 => keep-alive

长连接的好处是可以减少连接的建立和断开的开销，但是长连接仍然没有解决串行请求的问题。

	Http/2的特点：
	1. 多路复用
	2. 头部压缩
	3. 支持服务器推送/客户端推送/双向推送

而Http/2通过多路复用解决了串行请求的问题，多路复用允许在一个连接上同时发送多个请求。
Http/2中的连接阻塞问题是通过多路复用解决的，多路复用允许在一个连接上同时发送多个请求，这样就避免了请求阻塞的问题。
HTTP/2中，虽然通过多路复用技术解决了应用层面的队头阻塞问题，但在TCP层面仍然存在队头阻塞。

	Http/3
Http/3是基于QUIC协议的，QUIC协议是基于UDP协议的 ，而之前的HTTP协议都是基于TCP协议的。
QUIC协议可以解决TCP协议的一些问题，比如说连接的建立和断开的开销，以及连接的阻塞问题。
另外在文件上传过程中，网络环境切换时，TCP协议会导致文件上传失败，但QUIC支持快速连接建立，可以在单个往返中完成连接建立和密钥协商，甚至在第二次连接时实现0-RTT（零往返时间）的数据传输
