﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Common.Tools.VSTest
{
    /// <summary>
    /// Contains settings used by <see cref="VSTestRunner"/>.
    /// </summary>
    public sealed class VSTestSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets the settings filename to be used to control additional settings such as data collectors.
        /// </summary>
        public FilePath SettingsFile { get; set; }

        /// <summary>
        /// If set, specifies that the tests be executed in parallel. By default up to all available cores on the machine may be used. The number of cores to use may be configured using a settings file.
        /// </summary>
        public bool Parallel { get; set; }

        /// <summary>
        /// If set, enables data diagnostic adapter 'CodeCoverage' in the test run. Default settings are used if not specified using settings file.
        /// </summary>
        public bool EnableCodeCoverage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run tests within the vstest.console.exe process.
        /// This makes vstest.console.exe process less likely to be stopped on an error in the tests, but tests might run slower.
        /// Defaults to <c>false</c>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if running in isolation; otherwise, <c>false</c>.
        /// </value>
        public bool InIsolation { get; set; }

        /// <summary>
        /// If set, this makes VSTest use or skip the VSIX extensions installed (if any) in the test run.
        /// </summary>
        public bool? UseVsixExtensions { get; set; }

        /// <summary>
        /// This makes VSTest use custom test adapters from a given path (if any) in the test run.
        /// </summary>
        public DirectoryPath TestAdapterPath { get; set; }

        /// <summary>
        /// Gets or sets the target platform architecture to be used for test execution.
        /// </summary>
        public VSTestPlatform PlatformArchitecture { get; set; }

        /// <summary>
        /// Gets or sets the target .NET Framework version to be used for test execution.
        /// </summary>
        public VSTestFrameworkVersion FrameworkVersion { get; set; }

        /// <summary>
        /// Run tests that match the given expression, of the format &lt;property&gt;Operator&lt;value&gt;[|&amp;&lt;Expression&gt;]
        ///     where Operator is one of =, != or ~  (Operator ~ has 'contains'
        ///     semantics and is applicable for string properties like DisplayName).
        ///     Parenthesis () can be used to group sub-expressions.
        /// Examples: Priority=1
        ///           (FullyQualifiedName~Nightly|Name=MyTestMethod)
        /// </summary>
        public string TestCaseFilter { get; set; }

        /// <summary>
        /// Gets or sets the logger to use for test results.
        /// </summary>
        public VSTestLogger Logger { get; set; }

        /// <summary>
        /// If set, writes diagnosis trace logs to specified file.
        /// </summary>
        public FilePath Diag { get; set; }
    }
}