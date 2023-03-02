using System;
using System.Threading.Tasks;
using DevourNovelEngine.Prototype.Parser.RenPy.Analyzers;

namespace DevourNovelEngine.Prototype.Parser
{
    public static class Parser
    {
        public static System.Action<string> OnLog;


        public static void Parse(DocLines lines, params IAnalyzer[] analyzers)
        {
            for (; lines.CurrentLine != null; lines.Next())
            {
                try
                {
                    foreach (var analyzer in analyzers)
                    {
                        if (analyzer.TryAnalyze(lines))
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log($"parsing error occured on line {lines.Index} ({lines.CurrentLine}): {ex}");
                    throw;
                }

            }

            Log("Parsing completed");
        }

        private static long GetTimeStamp()
        {
            return System.Diagnostics.Stopwatch.GetTimestamp();
        }

        public static async Task ParseAsync(DocLines lines, params IAnalyzer[] analyzers)
        {
            long ts = GetTimeStamp();
            for (; lines.CurrentLine != null; lines.Next())
            {
                try
                {
                    lines.Trim();
                    foreach (var analyzer in analyzers)
                    {
                        if (analyzer.TryAnalyze(lines))
                            break;
                    }

                    long stamp = GetTimeStamp();
                    if (stamp - ts > 1_000_000)
                    {
                        ts = stamp;
                        Log(lines.Progress.ToString("N3"));
                        await Task.Yield();
                    }

                }
                catch (Exception ex)
                {
                    Log($"parsing error occured on line {lines.Index} ({lines.CurrentLine}): {ex}");
                    throw;
                }

            }

            Log("Parsing completed");
        }


        private static void Log(string msg)
        {
            OnLog?.Invoke(msg);
        }
    }
}
