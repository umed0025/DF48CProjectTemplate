using ConsoleAppFramework;
using Cysharp.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZLogger;

namespace DF48CProjectTemplate
{
    internal class Program : ConsoleAppBase
    {

        // C#8.0が使えないため、「<Nullable>enable</Nullable>」の設定は無理。
        // ConsoleAppFrammeworkを使用。（コンソール実行制御）
        // Zloggerを使用。（ログ制御、テキストログ、構造化ログ）
        // Contus.Fodyを使用。（単一実行ファイル）

        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureLogging(x =>
                {
                    x.ClearProviders();
                    x.SetMinimumLevel(LogLevel.Trace);
                    x.AddZLoggerConsole();
                    x.AddZLoggerFile("plain-text.log", "file-plain", o => { o.PrefixFormatter = (writer, info) => ZString.Utf8Format(writer, "[{0}]", info.Timestamp.ToLocalTime().DateTime); });
                    x.AddZLoggerFile("json.log", "file-structured", o => { o.EnableStructuredLogging = true; });
                })
                .RunConsoleAppFrameworkAsync<Program>(args);
#if DEBUG
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif
        }

        public void Run()
        {
            this.Context.Logger.LogInformation("Start");

            this.Context.Logger.LogInformation("Hello World.");

            this.Context.Logger.LogInformation("End");
        }
    }
}
