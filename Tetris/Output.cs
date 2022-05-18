using System;

public class Output
{
    public static void Matrix(char[,] ar, bool massege)
    {
        try
        {
            {
                for (int j = 0; j < ar.GetLength(1); j++)
                {
                    for (int i = 0; i < ar.GetLength(0); i++)
                    {
                        Console.CursorLeft = i * 2;
                        Console.Write(ar[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
        catch (NullReferenceException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\aМатрица не содержит элементов");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}  
