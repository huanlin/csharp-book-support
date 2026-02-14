// Demo: Multicast Delegate

Console.WriteLine("4. Multicast Delegate");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

// Use += to add methods
notifyAll += () => Console.WriteLine("  Notify Administrator");
notifyAll += () => Console.WriteLine("  Record Log");
notifyAll += () => Console.WriteLine("  Send Email");

Console.WriteLine("Executing all notifications:");
notifyAll?.Invoke();

// Remove one of the methods
Console.WriteLine("\nAfter removing 'Record Log':");
// Note: You cannot directly remove it here because the lambda is a new instance of an anonymous method.
// The example above is mainly to demonstrate the += syntax.
// To correctly remove it, you usually need a named method or to store the delegate instance.

// Example of correct removal:
Action logAction = () => Console.WriteLine("  [Named] Record Log");
notifyAll = null;
notifyAll += () => Console.WriteLine("  [Named] Notify Administrator");
notifyAll += logAction;

Console.WriteLine("Resetting and adding named delegate...");
notifyAll?.Invoke();

Console.WriteLine("\nAfter removing the named delegate:");
notifyAll -= logAction;
notifyAll?.Invoke();

Console.ReadKey();
