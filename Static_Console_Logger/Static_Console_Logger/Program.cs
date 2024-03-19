using System;
using System.Threading;

public static class Static_Logger
{
    public static void Static_Log(string static_logger_level, string static_logger_message)
    {
        if (static_logger_level == "Comment")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Comment: " + static_logger_message);
            Console.ResetColor();
        }
        else if (static_logger_level == "Warning")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Warning: " + static_logger_message);
            Console.ResetColor();
        }
        else if (static_logger_level == "Error")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + static_logger_message);
            Console.ResetColor();
            throw new Exception("Exit on Error Message: Wait for it...");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Static_Logger.Static_Log("Comment", "The worst vice is advice.");
            Static_Logger.Static_Log("Warning", "Too much screen time causes cancer and heart disease.");
            Static_Logger.Static_Log("Error", "You got this message because you ran this code.");
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