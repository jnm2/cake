// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Cake.Core
{
    /// <summary>
    /// Acts as a context providing info about the overall build prior to execution.
    /// </summary>
    public sealed class SetupContext : CakeContextAdapter, ISetupContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupContext"/> class.
        /// </summary>
        /// <param name="context">The Cake Context</param>
        /// <param name="tasksToExecute">The ordered list of tasks which are about to be executed with the target task listed last.</param>
        public SetupContext(ICakeContext context, IReadOnlyList<string> tasksToExecute)
            : base(context)
        {
            TasksToExecute = tasksToExecute;
        }

        /// <summary>
        /// Gets the ordered list of tasks which are about to be executed.
        /// The target task is listed last.
        /// </summary>
        public IReadOnlyList<string> TasksToExecute { get; }
    }
}
