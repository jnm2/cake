// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Common.Tools.Chocolatey.Upgrade
{
    using System.Globalization;

    /// <summary>
    /// The Chocolatey package upgrader.
    /// </summary>
    public sealed class ChocolateyUpgrader : ChocolateyTool<ChocolateyUpgradeSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChocolateyUpgrader"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The Chocolatey tool resolver.</param>
        public ChocolateyUpgrader(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IChocolateyToolResolver resolver)
            : base(fileSystem, environment, processRunner, tools, resolver)
        {
        }

        /// <summary>
        /// Upgrades Chocolatey packages using the specified settings.
        /// </summary>
        /// <param name="packageId">The source package id.</param>
        /// <param name="settings">The settings.</param>
        public void Upgrade(string packageId, ChocolateyUpgradeSettings settings)
        {
            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(packageId, settings));
        }

        private ProcessArgumentBuilder GetArguments(string packageId, ChocolateyUpgradeSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append("upgrade");
            builder.Append(packageId);

            // Debug
            if (settings.Debug)
            {
                builder.Append("-d");
            }

            // Verbose
            if (settings.Verbose)
            {
                builder.Append("-v");
            }

            // Accept License
            if (settings.AcceptLicense)
            {
                builder.Append("--acceptLicense");
            }

            // Always say yes, so as to not show interactive prompt
            builder.Append("-y");

            // Force
            if (settings.Force)
            {
                builder.Append("-f");
            }

            // Noop
            if (settings.Noop)
            {
                builder.Append("--noop");
            }

            // Limit Output
            if (settings.LimitOutput)
            {
                builder.Append("-r");
            }

            // Execution Timeout
            if (settings.ExecutionTimeout != 0)
            {
                builder.Append("--execution-timeout");
                builder.Append(settings.ExecutionTimeout.ToString(CultureInfo.InvariantCulture));
            }

            // Cache Location
            if (!string.IsNullOrWhiteSpace(settings.CacheLocation))
            {
                builder.Append("-c");
                builder.Append(settings.CacheLocation);
            }

            // Allow Unofficial
            if (settings.AllowUnofficial)
            {
                builder.Append("--allowunofficial");
            }

            // Package source
            if (!string.IsNullOrWhiteSpace(settings.Source))
            {
                builder.Append("-s");
                builder.Append(settings.Source);
            }

            // Version
            if (settings.Version != null)
            {
                builder.Append("--version");
                builder.Append(settings.Version);
            }

            // Prerelease
            if (settings.Prerelease)
            {
                builder.Append("--pre");
            }

            // Forcex86
            if (settings.Forcex86)
            {
                builder.Append("--x86");
            }

            // Install Arguments
            if (!string.IsNullOrWhiteSpace(settings.InstallArguments))
            {
                builder.Append("--ia");
                builder.Append(settings.InstallArguments);
            }

            // OverrideArguments
            if (settings.OverrideArguments)
            {
                builder.Append("-o");
            }

            // NotSilent
            if (settings.NotSilent)
            {
                builder.Append("--notSilent");
            }

            // Package Parameters
            if (!string.IsNullOrWhiteSpace(settings.PackageParameters))
            {
                builder.Append("--params");
                builder.Append(settings.PackageParameters);
            }

            // Allow Downgrade
            if (settings.AllowDowngrade)
            {
                builder.Append("--allowdowngrade");
            }

            // Side by side installation
            if (settings.SideBySide)
            {
                builder.Append("-m");
            }

            // Ignore Dependencies
            if (settings.IgnoreDependencies)
            {
                builder.Append("-i");
            }

            // Skip PowerShell
            if (settings.SkipPowerShell)
            {
                builder.Append("-n");
            }

            // Fail on Unfound
            if (settings.FailOnUnfound)
            {
                builder.Append("--failonunfound");
            }

            // Fail on Not Installed
            if (settings.FailOnNotInstalled)
            {
                builder.Append("--failonnotinstalled");
            }

            // User
            if (!string.IsNullOrWhiteSpace(settings.User))
            {
                builder.Append("-u");
                builder.Append(settings.User);
            }

            // Password
            if (!string.IsNullOrWhiteSpace(settings.Password))
            {
                builder.Append("-p");
                builder.Append(settings.Password);
            }

            // Ignore Checksums
            if (settings.IgnoreChecksums)
            {
                builder.Append("--ignorechecksums");
            }

            return builder;
        }
    }
}