## װ����ģʽ

### ����
װ����ģʽ��Decorator Pattern����һ�ֽṹ�����ģʽ���������ڲ��ı�������������£���̬�ظ�һ���������һЩ�����ְ��װ����ģʽ�ṩ�˱ȼ̳и��е��Ե��������������������ʱ��ӹ��ܡ�

### ���ó���
- ����Ҫ��һ������Ӷ����ְ��ʱ������ʹ��װ����ģʽ��


### ����ʾ��
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ����һ���ӿ�
public interface IComponent
{
	void Operation();
}

// ���������
public class ConcreteComponent : IComponent
{
	public void Operation()
	{
		Console.WriteLine("ConcreteComponent Operation");
	}
}

// ����װ������
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

// ����װ������A
public class ConcreteDecoratorA : Decorator
{
	public ConcreteDecoratorA(IComponent component) : base(component) { }
	public override void Operation()
	{
		base.Operation();
		Console.WriteLine("ConcreteDecoratorA Operation");
	}
}

// ����װ������B
public class ConcreteDecoratorB : Decorator
{
	public ConcreteDecoratorB(IComponent component) : base(component) { }
	public override void Operation()
	{
		base.Operation();
		Console.WriteLine("ConcreteDecoratorB Operation");
	}
}


// �ͻ��˴���
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