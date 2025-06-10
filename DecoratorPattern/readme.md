## 装饰器模式

### 定义
装饰器模式（Decorator Pattern）是一种结构型设计模式，它允许在不改变对象自身的情况下，动态地给一个对象添加一些额外的职责。装饰器模式提供了比继承更有弹性的替代方案，可以在运行时添加功能。

### 适用场景
- 当需要给一个类添加额外的职责时，可以使用装饰器模式。


### 代码示例
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 定义一个接口
public interface IComponent
{
	void Operation();
}

// 具体组件类
public class ConcreteComponent : IComponent
{
	public void Operation()
	{
		Console.WriteLine("ConcreteComponent Operation");
	}
}

// 抽象装饰器类
public abstract class Decorator : IComponent
{
	protected IComponent _component;
	public Decorator(IComponent component)
	{
		_component = component;
	}
	public virtual void Operation()
	{
		_component.Operation();
	}
}

// 具体装饰器类A
public class ConcreteDecoratorA : Decorator
{
	public ConcreteDecoratorA(IComponent component) : base(component) { }
	public override void Operation()
	{
		base.Operation();
		Console.WriteLine("ConcreteDecoratorA Operation");
	}
}

// 具体装饰器类B
public class ConcreteDecoratorB : Decorator
{
	public ConcreteDecoratorB(IComponent component) : base(component) { }
	public override void Operation()
	{
		base.Operation();
		Console.WriteLine("ConcreteDecoratorB Operation");
	}
}


// 客户端代码
public class Program
{
	public static void Main(string[] args)
	{
		IComponent component = new ConcreteComponent();
		component.Operation();
		IComponent decoratorA = new ConcreteDecoratorA(component);
		decoratorA.Operation();
		IComponent decoratorB = new ConcreteDecoratorB(decoratorA);
		decoratorB.Operation();
	}
}
```