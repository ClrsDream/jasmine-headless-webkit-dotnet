﻿using System;
using System.IO;

namespace jasmine_headless_webkit_dotnet
{
    public class LocalEnvironment : ILocalEnvironment
    {
        public virtual string GetJasmineConfigurationFileLocation()
        {
            return Path.Combine(GetRunDir(), "ScriptTests", "Support", "Jasmine.js");
        }

        public string GetJasmineConfigurationFileLocation(string configFile)
        {
            if (IsRelativePath(configFile))
            {
                return Path.Combine(GetRunDir(), configFile);                
            }
            return configFile;
        }

        private bool IsRelativePath(string file)
        {
            if (IsRunningOnWindows)
            {
                return file.Contains(Path.VolumeSeparatorChar.ToString());                
            }
            return file.StartsWith(Path.DirectorySeparatorChar.ToString());
        }

        public virtual string GetPhantomJSExeFileLocation()
        {
            return Path.Combine(GetToolsDir(), "phantomjs.exe");
        }

        public virtual string GetRunJasmineTestFileLocation()
        {
            return Path.Combine(GetToolsDir(), "run_jasmine_test.coffee");
        }

        public bool IsRunningOnWindows
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            }
        }

        public virtual string GetToolsDir()
        {
            if (IsRunningOnWindows)
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jasmine-headless-webkit-dotnet");
            }
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".jasmine-headless-webkit-dotnet");
        }

        public virtual string GetJasmineConsoleRunnerJSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "console-runner.js");
        }

        public virtual string GetJasmineJasmineHtmlJSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "jasmine-html.js");
        }

        public virtual string GetJasmineJasmineCSSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "jasmine.css");
        }

        public virtual string GetJasmineJasmineJSFileLocation(int jasmineVersion)
        {
            return Path.Combine(GetJasmineDir(jasmineVersion), "jasmine.js");
        }

        public virtual string GetJasmineDir(int jasmineVersion)
        {
            var major = Math.Abs(jasmineVersion/100);
            var minor = Math.Abs((jasmineVersion - (major*100))/10);
            var revision = Math.Abs((jasmineVersion - (major*100) - (minor*10)));
            var jasmineDir = string.Format("jasmine-{0}.{1}.{2}", major, minor, revision);
            return Path.Combine(GetToolsDir(), jasmineDir);
        }

        public virtual string GetRunDir()
        {
            return Environment.CurrentDirectory;
        }
    }

    public interface ILocalEnvironment
    {
        string GetPhantomJSExeFileLocation();
        string GetRunJasmineTestFileLocation();
        string GetToolsDir();
        string GetJasmineConsoleRunnerJSFileLocation(int jasmineVersion);
        string GetJasmineJasmineHtmlJSFileLocation(int jasmineVersion);
        string GetJasmineJasmineCSSFileLocation(int jasmineVersion);
        string GetJasmineJasmineJSFileLocation(int jasmineVersion);
        string GetJasmineDir(int jasmineVersion);
        string GetRunDir();
        string GetJasmineConfigurationFileLocation();
        bool IsRunningOnWindows { get; }
        string GetJasmineConfigurationFileLocation(string configFile);
    }
}