using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class KeyReader
{
    static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
    static CancellationToken token = cancelTokenSource.Token;
    public static ConsoleKeyInfo key;
    static bool press = false;
    bool KeyPress(ConsoleKeyInfo k, int ms)
    {
        CancellationTokenSource cancelTokenSource2 = new CancellationTokenSource();
        CancellationToken token2 = cancelTokenSource2.Token;
        //token = CancellationToken.None;
        key = k;
        Task RK = new Task(() =>
        {
            int mss = 0;
            ReadKey(cancelTokenSource2);
            while(mss < ms)
            {
                Thread.Sleep(30);
                mss += 30;
                if (token2.IsCancellationRequested)
                {
                    return;
                }
            }
        });
        RK.Start();
        Delay(ms, cancelTokenSource2);
        while(!token2.IsCancellationRequested)
        {
            Thread.Sleep(30);
        }
        return press;
    }
    static bool KeyPress(int ms, out ConsoleKeyInfo keypress)
    {
        press = false;
        Task RK = new Task(() =>
        {
            int mss = 0;
            ReadKeyAll();
            while (mss < ms)
            {
                Thread.Sleep(30);
                mss += 30;
                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        });
        RK.Start();
        Delay(ms);
        while (!token.IsCancellationRequested)
        {
            Thread.Sleep(30);
        }
        keypress = key;
        return press;
    }
    static void RestoreKey()
    {
        key = new ConsoleKeyInfo();
        press = false;
    }
    public static bool KeyPress(int ms)
    {
        RestoreKey();
        CancellationTokenSource cancelTokenSource3 = new CancellationTokenSource();
        CancellationToken token3 = cancelTokenSource3.Token;
        Task RK = new Task(() =>
        {
            int mss = 0;
            ReadKeyAll(cancelTokenSource3);
            while (mss < ms)
            {
                Thread.Sleep(30);
                mss += 30;
                if (token3.IsCancellationRequested)
                {
                    return;
                }
            }
        });
        RK.Start();
        Delay(ms, cancelTokenSource3);
        while (!token3.IsCancellationRequested)
        {
            //Task.Delay(30);
            Thread.Sleep(30);
        }
        return press;
    }
    public static bool KeyPress(int ms, int freq)
    {
        RestoreKey();
        CancellationTokenSource cancelTokenSource3 = new CancellationTokenSource();
        CancellationToken token3 = cancelTokenSource3.Token;
        Task RK = new Task(() =>
        {
            int mss = 0;
            ReadKeyAll(cancelTokenSource3);
            while (mss < ms)
            {
                Thread.Sleep(freq);
                mss += freq;
                if (token3.IsCancellationRequested)
                {
                    return;
                }
            }
        });
        RK.Start();
        Delay(ms, cancelTokenSource3);
        while (!token3.IsCancellationRequested)
        {
            Thread.Sleep(freq);
        }
        return press;
    }
    async static void ReadKey()
    {
        bool b;
        do
        {
            ConsoleKeyInfo k = await Task.Run(() => Console.ReadKey(true));
            b = k.Key == key.Key;
        }
        while (!b);
        press = true;
        cancelTokenSource.Cancel();
    }
    async static void ReadKey(CancellationTokenSource cancelTokenSource2)
    {
        bool b;
        do
        {
            ConsoleKeyInfo k = await Task.Run(() => Console.ReadKey(true));
            b = k.Key == key.Key;
        }
        while (!b);
        press = true;
        cancelTokenSource2.Cancel();
    }
    async static void ReadKeyAll()
    {
        bool b;
        ConsoleKeyInfo k = await Task.Run(() => Console.ReadKey(true));
        key = k;
        press = true;
        cancelTokenSource.Cancel();
    }
    async static void ReadKeyAll(CancellationTokenSource cancelTokenSource3)
    {
        ConsoleKeyInfo k = await Task.Run(() => Console.ReadKey(true));
        key = k;
        press = true;
        cancelTokenSource3.Cancel();
    }
    async static void Delay(int ms, CancellationTokenSource cancelTokenSourceN)
    {
        await Task.Delay(ms);
        cancelTokenSourceN.Cancel();
    }
    async static void Delay(int ms)
    {
        await Task.Delay(ms);
        cancelTokenSource.Cancel();
    }
}
