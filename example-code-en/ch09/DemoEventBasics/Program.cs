// Demo: Delegate vs. Event, and Basic Usage of Events

Console.WriteLine("=== Delegate vs. Event ===\n");

// --------------------------------------------------------------
// 1. Problems with Delegates
// --------------------------------------------------------------
Console.WriteLine("1. Why Events Are Needed (Problems with Delegates)");
Console.WriteLine(new string('-', 40));

// Demonstration of problems with delegates
var buttonWithDelegate = new ButtonWithDelegate();
buttonWithDelegate.Clicked += () => Console.WriteLine("  Subscriber A");
buttonWithDelegate.Clicked += () => Console.WriteLine("  Subscriber B");

Console.WriteLine("When using delegates, external code can do bad things:");
Console.WriteLine("  - Can use = to overwrite all subscribers");
Console.WriteLine("  - Can call Invoke() directly");

// --------------------------------------------------------------
// 2. Using Events (Protected Subscription)
// --------------------------------------------------------------
Console.WriteLine("\n2. Using Events (Protected Subscription)");
Console.WriteLine(new string('-', 40));

var button = new Button();
button.Clicked += () => Console.WriteLine("  Subscriber A received click notification");
button.Clicked += () => Console.WriteLine("  Subscriber B received click notification");

Console.WriteLine("Simulating button click:");
button.SimulateClick();

// button.Clicked = null;  // Compile error!
// button.Clicked.Invoke();  // Compile error!

Console.WriteLine("\n=== Example End ===\n");

// ============================================================
// Helper Classes
// ============================================================

public class ButtonWithDelegate
{
    public Action? Clicked;  // Public delegate field (unsafe)

    public void SimulateClick() => Clicked?.Invoke();
}

public class Button
{
    public event Action? Clicked;  // Using the 'event' keyword

    public void SimulateClick() => Clicked?.Invoke();
}
