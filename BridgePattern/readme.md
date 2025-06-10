## �Ž�ģʽ

### �����Ž�ģʽ
�Ž�ģʽ��Bridge Pattern����һ�ֽṹ�����ģʽ����ͨ�������󲿷�����ʵ�ֲ��ַ��룬ʹ���ǿ��Զ����ر仯���Ž�ģʽ�ĺ���˼���ǽ������ʵ�ֽ���Ӷ�ʹ�����߿��Զ�������չ��

### ���ó���
- ��һ����������������仯��ά��ʱ������ʹ���Ž�ģʽ��
- ����Ҫ�ڶ��ƽ̨��ʵ��ͬһ����ʱ������ʹ���Ž�ģʽ��
- ����Ҫ������ʱ�л�ʵ��ʱ������ʹ���Ž�ģʽ��
- ����Ҫ������ı�ը������ʱ������ʹ���Ž�ģʽ��

### ʹ��ʾ��
�ҽ��ṩһ��ʹ���Ž�ģʽ������ʾ����չʾ��ν�����ϵͳ���豸����������

```csharp
// ʵ�ֲ��ֵĽӿ�
public interface IDeviceDriver
{
    void Initialize();
    void ProcessData(string data);
    void Close();
}

// ����ʵ�֣���ӡ������
public class PrinterDriver : IDeviceDriver
{
    public void Initialize()
    {
        Console.WriteLine("��ʼ����ӡ������");
    }

    public void ProcessData(string data)
    {
        Console.WriteLine($"��ӡ�����ڴ�������: {data}");
    }

    public void Close()
    {
        Console.WriteLine("�رմ�ӡ������");
    }
}

// ����ʵ�֣�ɨ��������
public class ScannerDriver : IDeviceDriver
{
    public void Initialize()
    {
        Console.WriteLine("��ʼ��ɨ��������");
    }

    public void ProcessData(string data)
    {
        Console.WriteLine($"ɨ�������ڴ�������: {data}");
    }

    public void Close()
    {
        Console.WriteLine("�ر�ɨ��������");
    }
}

// ���󲿷�
public abstract class OperatingSystem
{
    protected IDeviceDriver driver;

    public OperatingSystem(IDeviceDriver driver)
    {
        this.driver = driver;
    }

    public abstract void HandleDevice(string operation);
}

// ��չ����Windows����ϵͳ
public class WindowsOS : OperatingSystem
{
    public WindowsOS(IDeviceDriver driver) : base(driver) { }

    public override void HandleDevice(string operation)
    {
        Console.WriteLine("Windowsϵͳ�����豸����");
        switch (operation.ToLower())
        {
            case "start":
                driver.Initialize();
                break;
            case "process":
                driver.ProcessData("Windowsϵͳ���ɵ�����");
                break;
            case "stop":
                driver.Close();
                break;
            default:
                Console.WriteLine("δ֪����");
                break;
        }
    }
}

// ��չ����Linux����ϵͳ
public class LinuxOS : OperatingSystem
{
    public LinuxOS(IDeviceDriver driver) : base(driver) { }

    public override void HandleDevice(string operation)
    {
        Console.WriteLine("Linuxϵͳ�����豸����");
        switch (operation.ToLower())
        {
            case "start":
                driver.Initialize();
                break;
            case "process":
                driver.ProcessData("Linuxϵͳ���ɵ�����");
                break;
            case "stop":
                driver.Close();
                break;
            default:
                Console.WriteLine("δ֪����");
                break;
        }
    }
}

// ʹ��ʾ��
public class Program
{
    public static void Main()
    {
        // ������ͬ������ʵ��
        IDeviceDriver printerDriver = new PrinterDriver();
        IDeviceDriver scannerDriver = new ScannerDriver();

        // ������ͬ�Ĳ���ϵͳ
        OperatingSystem windowsWithPrinter = new WindowsOS(printerDriver);
        OperatingSystem linuxWithScanner = new LinuxOS(scannerDriver);

        // ����Windowsϵͳʹ�ô�ӡ��
        Console.WriteLine("=== Windowsϵͳʹ�ô�ӡ�� ===");
        windowsWithPrinter.HandleDevice("start");
        windowsWithPrinter.HandleDevice("process");
        windowsWithPrinter.HandleDevice("stop");

        Console.WriteLine("\n=== Linuxϵͳʹ��ɨ���� ===");
        linuxWithScanner.HandleDevice("start");
        linuxWithScanner.HandleDevice("process");
        linuxWithScanner.HandleDevice("stop");

        // ��ʾ����ԣ�Windowsʹ��ɨ����
        Console.WriteLine("\n=== Windowsϵͳʹ��ɨ���� ===");
        OperatingSystem windowsWithScanner = new WindowsOS(scannerDriver);
        windowsWithScanner.HandleDevice("start");
        windowsWithScanner.HandleDevice("process");
        windowsWithScanner.HandleDevice("stop");
    }
}
```

���ʾ��չʾ���Ž�ģʽ�ļ����ؼ��ص㣺

1. **������ʵ�ַ���**��
   - `OperatingSystem` �ǳ��󲿷�
   - `IDeviceDriver` ��ʵ�ֲ���
   - ���߿��Զ����仯����չ

2. **˫�����**��
   - ����ϵͳ�㣺`OperatingSystem` -> `WindowsOS`, `LinuxOS`
   - ��������㣺`IDeviceDriver` -> `PrinterDriver`, `ScannerDriver`

3. **������**��
   - �κβ���ϵͳ������ʹ���κ��豸����
   - ����������ʱ��̬�л�ʵ��

4. **������չ**��
   - ��������µĲ���ϵͳ����Ӱ��������������
   - ��������µ������������Ӱ�����в���ϵͳ

������δ����������������Ľ����

```
=== Windowsϵͳʹ�ô�ӡ�� ===
Windowsϵͳ�����豸����
��ʼ����ӡ������
Windowsϵͳ�����豸����
��ӡ�����ڴ�������: Windowsϵͳ���ɵ�����
Windowsϵͳ�����豸����
�رմ�ӡ������

=== Linuxϵͳʹ��ɨ���� ===
Linuxϵͳ�����豸����
��ʼ��ɨ��������
Linuxϵͳ�����豸����
ɨ�������ڴ�������: Linuxϵͳ���ɵ�����
Linuxϵͳ�����豸����
�ر�ɨ��������

=== Windowsϵͳʹ��ɨ���� ===
Windowsϵͳ�����豸����
��ʼ��ɨ��������
Windowsϵͳ�����豸����
ɨ�������ڴ�������: Windowsϵͳ���ɵ�����
Windowsϵͳ�����豸����
�ر�ɨ��������
```

���ʾ���ܺõ�չʾ���Ž�ģʽ��ν��"�౬ը"���⣬���ṩ��������չ�ԡ�