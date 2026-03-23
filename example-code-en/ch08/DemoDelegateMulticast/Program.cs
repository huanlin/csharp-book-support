// Demo: Multicast Delegate

Console.WriteLine("4. Multicast Delegate");
Console.WriteLine(new string('-', 40));

Action? notifyAll = null;

Action notifyAdmin = () => Console.WriteLine("  Notify Administrator");
Action writeLog = () => Console.WriteLine("  Record Log");
Action sendMail = () => Console.WriteLine("  Send Email");

// Use += to add methods
notifyAll += notifyAdmin;
notifyAll += writeLog;
notifyAll += sendMail;

Console.WriteLine("Executing all notifications:");
notifyAll?.Invoke();

Console.WriteLine("\nAfter removing 'Send Email':");
notifyAll -= sendMail;
notifyAll?.Invoke();

Console.ReadKey();
