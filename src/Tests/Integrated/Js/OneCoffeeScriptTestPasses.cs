﻿using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Js
{
    [TestClass, Ignore]
    public class OneCoffeeScriptTestPasses
    {
        private static bool runSucceeded;
        private static Test test;

        [ClassInitialize]
        public static void RunFiles(TestContext testContext)
        {
            var sourceFile = Path.Combine("JasmineTests", "Scripts", "calculator.coffee");
            var sourceFiles = new[] {sourceFile};
            var testFile = Path.Combine("JasmineTests", "ScriptTests", "calculatorSumPassSpec.coffee");
            var testFiles = new[] {testFile};
            test = RunTestHelper.RunTestWithJSFiles(sourceFiles, testFiles);
            runSucceeded = test.Run();
        }

        [TestMethod]
        public void VerifyPass()
        {
            runSucceeded.Should().BeTrue();
        }
        [TestMethod]
        public void VerifySuccesses()
        {
            test.NumberOfSuccesses.Should().Be(1);
        }
        [TestMethod]
        public void VerifyNoFailures()
        {
            test.NumberOfFailures.Should().Be(0);
        }
    }
}
