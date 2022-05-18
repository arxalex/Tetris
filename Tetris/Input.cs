using System;

public class Input
{
    public static int Int(string str)
    {
        bool flag = true;
        string read;
        int i;
        int len = 0;
        int curl = Console.CursorLeft;
        int curt = Console.CursorTop;
        do
        {
            try
            {
                if (!flag)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(curl, curt);
                    Console.Write(str);
                    for (int j = 0; j < len; j++)
                    {
                        Console.Write(' ');
                    }
                    Console.SetCursorPosition(Console.CursorLeft - len, Console.CursorTop);
                    read = Console.ReadLine();
                    len = read.Length;
                    i = int.Parse(read);
                    flag = true;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.SetCursorPosition(curl, curt);
                    Console.Write(str);
                    read = Console.ReadLine();
                    len = read.Length;
                    i = int.Parse(read);
                    flag = true;
                }
            }
            catch (FormatException)
            {
                Console.Write("\a");
                flag = false;
                i = 0;
            }
            catch (OverflowException)
            {
                Console.Write("\a");
                flag = false;
                i = 0;
            }
        }
        while (!flag);
        return i;
    }   
}