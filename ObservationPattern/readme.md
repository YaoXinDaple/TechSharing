## 观察者模式

### 定义
观察者模式是一种行为设计模式，它定义了一种一对多的依赖关系，使得当一个对象（主题）状态发生变化时，所有依赖于它的对象（观察者）都得到通知并自动更新。

### 被观察者一般需要实现的接口
```csharp
public interface ISubject
{
	void Register(IObserver observer);
	void UnSubscribe(IObserver observer);
	void Notify();
}
```

### 观察者一般需要实现的接口
```csharp
public interface IObserver
{
	void Update(string message);
}
```

具体到细节中，有一个实现是观察者要引用被观察者，从而实现观察者可以随时了解要观察的结果的当前值。

例如：
气象站作为被观察者，湿度、温度、气压传感器的读数是观察者内部管理的状态。
气象App作为观察者，订阅气象站的变化，当气象站的状态发生变化时，气象App会收到通知并更新显示。而且App还需要随时知道气象站的当前状态，以便在没有变化时也能显示最新数据。