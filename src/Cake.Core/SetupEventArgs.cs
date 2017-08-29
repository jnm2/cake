﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Cake.Core
{
    /// <summary>
    /// Event data for the <see cref="ICakeEngine.Setup"/> event.
    /// </summary>
    public sealed class SetupEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the Cake context.
        /// </summary>
        public ISetupContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupEventArgs"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SetupEventArgs(ISetupContext context)
        {
            Context = context;
        }
    }
}
