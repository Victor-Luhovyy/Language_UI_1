using CultureSwitcherApp;
using System;
using System.Threading;

public class BackgroundThread
{
    public void StartThread()
    {
        var thread = new Thread(() =>
        {
            // Simulate some work
            Thread.Sleep(2000);

            // Update the culture from a worker thread
            CultureHelper.ChangeCulture("ar-SA");
        });
        thread.IsBackground = true;
        thread.Start();
    }
}
