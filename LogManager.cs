/* 
 * 
 * LogManager.cs
 * Author Stephanie Schutz for Xtricate.
 * Writes string to log file in exe directory.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

public static class LogManager
{
    static string filename = "log.txt";
    static string path = "";
    static StreamWriter sw;
    static bool logUsed = false;

    //public LogManager()
    //{
    //    filename = "log.txt";
    //    string workingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    //    path = Path.Combine(workingDirectory, filename);
    //    logUsed = false;
    //}

    public static void WriteString(string message, params object[] parameters)
    {
        WriteString(String.Format(message, parameters));

    }

    public static void WriteString(string message)
    {
        OpenLogFile();
        string caller = new StackTrace().GetFrame(1).GetMethod().Name;
        sw.WriteLine("{0:G} {1}:: {2}\n", System.DateTime.Now, caller, message);
        logUsed = true;
        CloseLogFile();
    }

    public static long LogLength()
    {
        FileInfo f = new FileInfo(filename);
        return f.Length;
    }

    /// <summary>
    /// Was the log written to?
    /// </summary>
    /// <returns>true or false</returns>
    public static bool WroteToLog()
    {
        return logUsed;
    }

    private static void Init()
    {
        OpenLogFile();
        CloseLogFile();
    }

    public static string GetPath()
    {
        if (path.Equals(""))
            Init();

        return path;
    }

    public static string GetFileName()
    {
        return filename;
    }

    /// <summary>
    /// Creates or opens log file.
    /// </summary>
    private static void OpenLogFile()
    {
        string workingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(workingDirectory, filename);
        logUsed = false;

        workingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        path = Path.Combine(workingDirectory, filename);

        // Create a writer and open the file:
        if (!File.Exists(path))
            sw = new StreamWriter(path);
        else
            sw = File.AppendText(path);

        //Log.Write("{0:G}.", System.DateTime.Now);
    }

    /// <summary>
    /// Closes the log file.
    /// </summary>
    private static void CloseLogFile()
    {
        if (File.Exists(filename))
        {
            sw.Close();
        }
    }
}
