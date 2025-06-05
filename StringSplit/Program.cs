StructWithManagedType anotherStruct = new StructWithManagedType { Value = 1, Name = "123" };

PureUnManagedStruct pureUnManagedStruct = new PureUnManagedStruct { Value = 1 };
StructWithGenericType<PureUnManagedStruct> someStruct = new StructWithGenericType<PureUnManagedStruct> { Value = 1, Number = 1.2, SomValue = anotherStruct };
PrintScreen(someStruct);

Console.ReadKey();



void PrintScreen<T>(T someStruct) where T : unmanaged
{
    Console.WriteLine(someStruct);
}

public struct PureUnManagedStruct
{
    public int Value;
}

public struct StructWithManagedType
{
    public int Value;
    public string Name;
}

public struct StructWithGenericType<T> where T : unmanaged
{
    public int Value;
    public double Number;
    public T SomValue;
}
