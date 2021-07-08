using System;
using System.IO;


internal class Constructor : IDisposable
{
    internal enum Severity
    {
        race, debug, information, warning, error, critical
    }

    internal struct logs
    {
        public string data { get; set; }
        public Constructor.Severity severity { get; set; }
    }

    internal Constructor(string filepath)
    {
        files = new StreamWriter(filepath, true);//открыть поток на запись + запись в конец файла 
    }

    public static void Log(string data, Severity severity)//Реализация заданной функции 
    {
        files.Write($"{Environment.NewLine}[{DateTime.Now}] [{severity}]: {data}");//запись 

    }

    public void Dispose()
    {
        files.Flush();
        files.Close();
        files.Dispose();
    }

    private static StreamWriter files;
}

class Pragma
{
    Constructor Logger { get; set; }


    static Constructor.logs log;

    static Random rand = new Random();

    static Array severnityEnum = Enum.GetValues(typeof(Constructor.Severity));

    public void Init(string path)
    {
        Logger = new Constructor(path);
    }

    private void Rand1()
    {
        log.data = "Test 1";
        log.severity = (Constructor.Severity)severnityEnum.GetValue(rand.Next(severnityEnum.Length));
    }
    private static void Rand2()
    {
        log.data = "Test 2";
        log.severity = (Constructor.Severity)severnityEnum.GetValue(rand.Next(severnityEnum.Length));
    }

    private static void Rand3()
    {
        log.data = "Test 3";
        log.severity = (Constructor.Severity)severnityEnum.GetValue(rand.Next(severnityEnum.Length));
    }

    public int Fill()
    {
        try
        {
            if (Logger == null) throw new DirectoryNotFoundException("Null-error");
            int a;
            Random rand = new Random();
            a = rand.Next(-30,60) ;

            if (a > 30)
                Rand1();

            else if (a <= 30 && a > 0)
                Rand2();
            else
                Rand3();

            Constructor.Log(log.data, log.severity);
            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return -1;
        }
    }

    public void Dispose()
    {
        Logger.Dispose();
        Logger = null;
    }
}

namespace Prac2
{
    class Program
    {
        static void Main(string[] args)
        {
            Pragma pragma = new Pragma();

            pragma.Init("testPrac2.txt");

            pragma.Fill();
            pragma.Fill();
            pragma.Fill();
            pragma.Fill();
            pragma.Dispose();
            StreamReader str = new StreamReader("testPrac2.txt");
            string data = str.ReadToEnd();
            string[] dataArray = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dataArray.Length; i++) Console.WriteLine(dataArray[i]);

            str.Close();
        }
    }


}
