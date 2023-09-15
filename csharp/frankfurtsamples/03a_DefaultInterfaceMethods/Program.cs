using System.Threading.Tasks;

ILogger logger = new ConsoleLogger();
logger.Log("message");
logger.Log(new Exception("sample exception"));

IEnumerableEx<string> names = new MyCollection<string> { "James", "Jack", "Jochen", "Sebastian", "Lewis", "Juan" };

var jNames = names.Where(n => n.StartsWith("J"));
foreach (var name in jNames)
{
    Console.WriteLine(name);
}

// mixin sample

OverheadLight ol = new();
ol.SwitchOn();
ol.SwitchOff();
if (ol is ITimerLight timerLight)
{
    await timerLight.TurnOnFor(1000);
}
if (ol is IBlinkingLight blinkingLight)
{
    await blinkingLight.Blink(200, 3);
}

// interface with no implementation
public interface ILight
{
    void SwitchOn();
    void SwitchOff();
    bool IsOn();
}

// Timer light with default implementation
public interface ITimerLight : ILight
{
    public async Task TurnOnFor(int duration)
    {
        Console.WriteLine("timer turned on");
        SwitchOn();
        await Task.Delay(duration);
        SwitchOff();
        Console.WriteLine("timer turned off");
    }
}

// IBlinkingLight with default implementation
public interface IBlinkingLight : ILight
{
    public async virtual Task Blink(int duration, int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            Console.WriteLine($"blinking turned on {i}");
            SwitchOn();
            await Task.Delay(duration);
            SwitchOff();
            Console.WriteLine("blinking turned off");
        }
    }
}

// OverheadLigth with blinking and timer light
// and a custom implementation of blink
public class OverheadLight : ILight, IBlinkingLight, ITimerLight
{
    private bool _isOn;
    public bool IsOn() => _isOn;

    public void SwitchOff() => _isOn = false;

    public void SwitchOn() => _isOn = true;

    async public Task Blink(int duration, int repeatCount)
    {
        await (this as IBlinkingLight).Blink(duration, repeatCount);  
        await Task.Delay(duration);
        Console.WriteLine("custom blink");
    }
}
