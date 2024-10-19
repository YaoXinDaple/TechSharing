## MediatR

### MediatR 是中介者模式的一个实现
	作用：
		调用方和被调用方之间的解耦
		将请求和处理分离
		CQRS的一种实现方式（命令查询与职责分离）


### MediatR提供的Behavior管道
	可以结合FluentValidation对所有Request进行验证

### 使用中间件和HttpContext.Response.Items 实现在返回客户端之后进行记录操作日志等操作
	理论上可以作为DomainEvent的一种处理模式
	相比较InBox,OutBox模式，降低了系统复杂度，但可靠性还是需要借助其他手段来保证

