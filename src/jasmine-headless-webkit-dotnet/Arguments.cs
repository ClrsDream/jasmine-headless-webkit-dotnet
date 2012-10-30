using System;
using System.ComponentModel;
using Args;
using Args.Help;
using Args.Help.Formatters;

namespace jasmine_headless_webkit_dotnet
{
    [Description("Jasmine Runner based on .NET")]
    public class Arguments
    {
        public Arguments()
        {
            VerbosityLevel = VerbosityLevel.Normal;
        }
        [Description("This help text.")]
        public bool Help { get; set; }
        [Description("The directory to run the server from.")]
        public string Directory { get; set; }
        [Description("The port to run the server at.")]
        public int? Port { get; set; }
        [Description("The test archive.")]
        public string FileName { get; set; }
        [Description("The time to wait for the tests to complete before stopping.")]
        public int? Timeout { get; set; }
        [Description("The verbosity level. Defaults to normal. Possible values: normal, verbose.")]
        public VerbosityLevel VerbosityLevel { get; set; }
        [Description("Weather to run the server. Defaults to false.")]
        public bool RunServer { get; set; }
        [Description("Javascript files to test.")]
        public string[] TestFiles { get; set; }
        [Description("Javascript source files.")]
        public string[] SourceFiles { get; set; }

        public RunType RunType
        {
            get
            {
                if (IsHelpRun())
                {
                    return RunType.Help;
                }
                if (IsJsRun())
                {
                    return RunType.JSFiles;
                }
                if (IsHtmlRun())
                {
                    return RunType.HtmlFile;
                }
                return RunType.Help;
            }
        }

        private bool IsHtmlRun()
        {
            return (!string.IsNullOrEmpty(Directory) && !string.IsNullOrEmpty(FileName));
        }

        private bool IsHelpRun()
        {
            return Help;
        }

        private bool IsJsRun()
        {
            return ((TestFiles != null && TestFiles.Length > 0) && (SourceFiles != null && SourceFiles.Length > 0));
        }

        public int GetPort()
        {
            return Port ?? new Random().Next(5000, 9000);
        }

        public int GetTimeOut()
        {
            return Timeout ?? 60;
        }


        public bool IsConsistent()
        {
            return RunType != RunType.Help;
        }

        public void WriteHelp()
        {
            var help = new HelpProvider().GenerateModelHelp(Configuration.Configure<Arguments>());
            var f = new ConsoleHelpFormatter(Console.BufferWidth, 1, 5);
            Console.WriteLine(f.GetHelp(help));
        }
    }
}