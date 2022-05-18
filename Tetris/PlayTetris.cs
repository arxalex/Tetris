using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class PlayTetris
{
    static char empty = ' ';
    static char fill = '■';
    static char[,] field;
    static int x;
    static int y;
    static int speed = 661;
    static int screenfreq = 100;
    static int pressfreq = 660;
    static int pressfreq2 = 110;
    static double score;
    public static bool GO;
    static int xs, ys;
    static int TimerPress;
    static Random random = new Random();
    static void AddField()
    {
        field = new char[x, y];
        for (int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                field[i, j] = empty;
            }
        }
    }
    static char[,] RandomShape()
    {
        int i = random.Next(0, 11);
        char[,] Shape;
        switch (i)
        {
            case 0:
                {
                    char[,] RS = { { fill, fill, fill }, { empty, fill, empty } };
                    Shape = RS;
                }
                break;
            case 1:
                {
                    char[,] RS = { { fill, empty }, { fill, fill } };
                    Shape = RS;
                }
                break;
            case 2:
                {
                    char[,] RS = { { fill, fill, fill, fill } };
                    Shape = RS;
                }
                break;
            case 3:
                {
                    char[,] RS = { { fill, fill } };
                    Shape = RS;
                }
                break;
            case 4:
                {
                    char[,] RS = { { fill, fill }, { fill, fill } };
                    Shape = RS;
                }
                break;
            case 5:
                {
                    char[,] RS = { { fill, fill, fill } };
                    Shape = RS;
                }
                break;
            case 6:
                {
                    char[,] RS = { { fill, fill, fill }, { fill, empty, empty } };
                    Shape = RS;
                }
                break;
            case 7:
                {
                    char[,] RS = { { fill, fill, fill }, { empty, empty, fill } };
                    Shape = RS;
                }
                break;
            case 8:
                {
                    char[,] RS = { { fill, fill, empty }, { empty, fill, fill } };
                    Shape = RS;
                }
                break;
            case 9:
                {
                    char[,] RS = { { empty, fill, fill }, { fill, fill, empty } };
                    Shape = RS;
                }
                break;
            default:
                {
                    char[,] RS = { { fill } };
                    Shape = RS;
                    break;
                }
        }
        return Shape;
    }
    static char[,] Turn(char[,] Shape)
    {
        int n = Shape.GetLength(0);
        int m = Shape.GetLength(1);
        char[,] TShape = new char[m,n];
        for(int j = 0; j < n; j++)
        {
            for(int i = 0; i < m; i++)
            {
                TShape[i, j] = Shape[(n - 1) - j, i];
            }
        }
        return TShape;
    }
    static void Turnshape(ref char[,] shape)
    {
        char[,] shape2 = Turn(shape);
        int u = shape.GetLength(0);
        int v = shape.GetLength(1);
        int u2 = shape2.GetLength(0);
        int v2 = shape2.GetLength(1);
        for (int j = ys; j < ys + v; j++)
        {
            for (int i = xs; i < xs + u; i++)
            {
                if (shape[i - xs, j - ys] == fill)
                {
                    field[i, j] = empty;
                }
            }
        }
        if (xs + u2 > x)
        {
            xs = x - u2;
        }
        while (WillCover(shape2))
        {
            ys--;
        }
        for (int j = ys; j < ys + v2; j++)
        {
            for (int i = xs; i < xs + u2; i++)
            {
                if(shape2[i - xs, j - ys] == fill)
                {
                    field[i, j] = shape2[i - xs, j - ys];
                }
            }
        }
        shape = shape2;
    }
    static char[,] RandomTurnShape()
    {
        char[,] shape = RandomShape();
        int turn = random.Next(0, 4);
        for (int i = 0; i < turn; i++)
        {
            shape = Turn(shape);
        }
        return shape;
    }
    static void Spawn (char[,] shape)
    {
        int u = shape.GetLength(0);
        int v = shape.GetLength(1);
        for (int j = 0; j < v; j++)
        {
            for (int i = xs; i < xs + u; i++)
            {
                field[i, j] = shape[i - xs, j];
            }
        }
    }
    static void Down(char[,] shape)
    {
        if (!WillCover(shape))
        {
            int u = shape.GetLength(0);
            int v = shape.GetLength(1);
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    if (shape[i - xs, j - ys] == fill)
                    {
                        field[i, j] = empty;
                    }
                }                
            }
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    if (shape[i - xs, j - ys] == fill)
                    {
                        field[i, j + 1] = fill;
                    }
                }
            }
            ys++;
        }
    }
    static bool WillCover(char[,] shape)
    {
        int u = shape.GetLength(0);
        int v = shape.GetLength(1);
        bool b = (ys + v) >= y;
        if (!b)
        {
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    try
                    {
                        if ((shape[i - xs, j - ys] == fill) && (field[i, j + 1] == fill) && (shape[i - xs, j - ys + 1] != fill))
                        {
                            b = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        if ((shape[i - xs, j - ys] == fill) && (field[i, j + 1] == fill))
                        {
                            b = true;
                        }
                    }
                }
            }
        }
        return b;
    }
    static void Left(char[,] shape)
    {
        if (!WillCoverLeft(shape))
        {
            int u = shape.GetLength(0);
            int v = shape.GetLength(1);
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    if (shape[i - xs, j - ys] == fill)
                    {
                        field[i, j] = empty;
                    }
                }
            }
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    if (shape[i - xs, j - ys] == fill)
                    {
                        field[i - 1, j] = fill;
                    }
                }
            }
            xs--;
        }
    }
    static void Right(char[,] shape)
    {
        if (!WillCoverRight(shape))
        {
            int u = shape.GetLength(0);
            int v = shape.GetLength(1);
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    if (shape[i - xs, j - ys] == fill)
                    {
                        field[i, j] = empty;
                    }
                }
            }
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    if (shape[i - xs, j - ys] == fill)
                    {
                        field[i + 1, j] = shape[i - xs, j - ys];
                    }
                }
            }
            xs++;
        }
    }
    static bool WillCoverLeft(char[,] shape)
    {
        int u = shape.GetLength(0);
        int v = shape.GetLength(1);
        bool b = xs <= 0;
        if (!b)
        {
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    try
                    {
                        if ((shape[i - xs, j - ys] == fill) && (field[i - 1, j] == fill) && (shape[i - xs - 1, j - ys] != fill))
                        {
                            b = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        if ((shape[i - xs, j - ys] == fill) && (field[i - 1, j] == fill))
                        {
                            b = true;
                        }
                    }
                }
            }
        }
        return b;
    }
    static bool WillCoverRight(char[,] shape)
    {
        int u = shape.GetLength(0);
        int v = shape.GetLength(1);
        bool b = xs + u >= x;
        if (!b)
        {
            for (int j = ys; j < ys + v; j++)
            {
                for (int i = xs; i < xs + u; i++)
                {
                    try
                    {
                        if ((shape[i - xs, j - ys] == fill) && (field[i + 1, j] == fill) && (shape[i - xs + 1, j - ys] != fill))
                        {
                            b = true;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        if ((shape[i - xs, j - ys] == fill) && (field[i + 1, j] == fill))
                        {
                            b = true;
                        }
                    }
                }
            }
        }
        return b;
    }
    async static void Displayer()
    {
        int curl = Console.CursorLeft;
        int curt = Console.CursorTop; 
        while (!GO)
        {
            Console.SetCursorPosition(curl, curt);
            await Task.Run(() => Output.Matrix(field, false));
            await Task.Delay(screenfreq);
        }
    }
    static bool GameOver(char[,] shape)
    {
        bool gameover = (ys == 0) && WillCover(shape);
        if(gameover)
        {
            Console.SetWindowSize(30, y + 4);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Gameover, your score = {0}", score);
        }
        return gameover;
    }
    static void Clear(double count)
    {
        bool clear = false;
        bool isfill;
        int ycl = 0;
        for (int j = 0; (j < y) && !clear; j++)
        {
            isfill = field[0, j] == fill;
            for (int i = 1; (i < x) && isfill; i++)
            {
                isfill = field[i, j] == fill;
                clear = (i == x - 1) && isfill;
            }
            if (clear)
            {
                ycl = j;
            }
        }
        if (clear)
        {
            for (int j = ycl; j > 1; j--)
            {
                for (int i = 0; i < x; i++)
                {
                    field[i, j] = field[i,j - 1];
                }
            }
            score = score - count * count + (++count) * count;
            Clear(count);
        }      
    }
    public static void Play()
    {
        score = 0;
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        x = Input.Int("Side x: ");
        y = Input.Int("Side y: ");
        Console.CursorVisible = false;
        AddField();
        Console.SetWindowSize(x * 2, y + 1);
        if (x > 8)
        {
            Console.SetBufferSize(x * 2, y + 3);
        }
        else
        {
            Console.SetBufferSize(16, y + 3);
        }
        char[,] shape;
        GO = false;
        Displayer();
        while (!GO)
        {
            shape = RandomTurnShape();
            ys = 0;
            xs = x / 2 - 1;
            Spawn(shape);
            //Output.Matrix(field, true);
            while(!WillCover(shape))
            {
                TimerPress = 0;
                while(TimerPress < speed)
                {
                    Press(ref shape);
                    //Output.Matrix(field, true);
                    TimerPress += pressfreq;
                }
                Down(shape);
                //Output.Matrix(field, true);
            }
            Clear(0);
            //Output.Matrix(field, true);
            GO = GameOver(shape);
        }
    }
    static void Press(ref char[,] shape)
    {
        ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        bool press = KeyReader.KeyPress(pressfreq, pressfreq2);
        if (press)
        {
            keypress = KeyReader.key;
            if (keypress.Key == ConsoleKey.DownArrow)
            {
                Down(shape);
            }
            else
            {
                if (keypress.Key == ConsoleKey.LeftArrow)
                {
                    Left(shape);
                }
                else
                {
                    if (keypress.Key == ConsoleKey.RightArrow)
                    {
                        Right(shape);
                    }
                    else
                    {
                        if (keypress.Key == ConsoleKey.UpArrow)
                        {
                            Turnshape(ref shape);
                        }
                    }
                }
            }
        }
    }
}
     
    
