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
	˵��Http/2����������Ҫ�˽�һ��Http/1.1�����⣬Http/1.1��������Ҫ�У�
	1. ���е����󣬵�����������
	2. ����ͷ���࣬���´���Ч�ʵ�

���������Դͷ����HttpЭ���ǻ���TCPЭ��ģ���TCPЭ����һ���������ӵ�Э�飬������һ��������ֻ�ܷ���һ��������͵����˴�������

	Http/1.1���Http/1.0�ĸĽ���
	1. ֧�ֳ����� => keep-alive

�����ӵĺô��ǿ��Լ������ӵĽ����ͶϿ��Ŀ��������ǳ�������Ȼû�н��������������⡣

	Http/2���ص㣺
	1. ��·����
	2. ͷ��ѹ��
	3. ֧�ַ���������/�ͻ�������/˫������

��Http/2ͨ����·���ý���˴�����������⣬��·����������һ��������ͬʱ���Ͷ������
Http/2�е���������������ͨ����·���ý���ģ���·����������һ��������ͬʱ���Ͷ�����������ͱ������������������⡣
HTTP/2�У���Ȼͨ����·���ü��������Ӧ�ò���Ķ�ͷ�������⣬����TCP������Ȼ���ڶ�ͷ������

	Http/3
Http/3�ǻ���QUICЭ��ģ�QUICЭ���ǻ���UDPЭ��� ����֮ǰ��HTTPЭ�鶼�ǻ���TCPЭ��ġ�
QUICЭ����Խ��TCPЭ���һЩ���⣬����˵���ӵĽ����ͶϿ��Ŀ������Լ����ӵ��������⡣
�������ļ��ϴ������У����绷���л�ʱ��TCPЭ��ᵼ���ļ��ϴ�ʧ�ܣ���QUIC֧�ֿ������ӽ����������ڵ���������������ӽ�������ԿЭ�̣������ڵڶ�������ʱʵ��0-RTT��������ʱ�䣩�����ݴ���
