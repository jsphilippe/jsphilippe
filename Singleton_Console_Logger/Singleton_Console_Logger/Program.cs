using System;
using System.Threading;
using Ninject;

public interface INinject_Logger
{
    void Singleton_Log(string reporting_level, string console_message);
}

public sealed class Singleton_Logger : INinject_Logger
{
    private static INinject_Logger singleton_instance = null;
    private static readonly object Singleton_Object = new object();
    private Singleton_Logger() { }

    public static INinject_Logger Instance
    {
        get
        {
            lock (Singleton_Object)
            {
                if (singleton_instance == null)
                {
                    singleton_instance = new Singleton_Logger();
                }

                return singleton_instance;
            }
        }
    }

    public void Singleton_Log(string reporting_level, string console_message)
    {
        if (reporting_level == "Comment")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Comment: " + console_message);
        }
        else if (reporting_level == "Warning")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + console_message);
        }
        else if (reporting_level == "Error")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + console_message);
            Console.ResetColor();
            throw new Exception("Exit on Error Message: Wait for it...");
        }
    }
}

class CountDown
{
    static void Main()
    {
        try
        {
            IKernel ninject_kernel = new StandardKernel();
            ninject_kernel.Bind<INinject_Logger>().ToConstant(Singleton_Logger.Instance);
            INinject_Logger ninject_Logger = ninject_kernel.Get<INinject_Logger>();
            ninject_Logger.Singleton_Log("Comment", "The worst vice is advice.");
            ninject_Logger.Singleton_Log("Warning", "Too much screen time causes cancer and heart disease.");
            ninject_Logger.Singleton_Log("Error", "You got this message because you ran this code.");
        }
        catch (Exception Caught_Ya)
        {
            Console.WriteLine(Caught_Ya.Message);
            int i = 10;
            bool isTic = true;
            do
            {
                Console.WriteLine(i-- + (isTic ? " tic" : " toc"));
                isTic = !isTic;
                Thread.Sleep(1500);
            }
            while (i > 0);
            Console.WriteLine("Th-th-th-that's all, folks!");
        }
    }
}