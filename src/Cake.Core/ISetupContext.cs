// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Cake.Core
{
    /// <summary>
    /// Acts as a context providing info about the overall build prior to execution.
    /// </summary>
    public interface ISetupContext : ICakeContext
    {
        /// <summary>
        /// Gets the ordered list of tasks which are about to be executed.
        /// The target task is listed last.
        /// </summary>
        IReadOnlyList<string> TasksToExecute { get; }
    }
}
