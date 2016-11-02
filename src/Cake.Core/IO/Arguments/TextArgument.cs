﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Cake.Core.IO.Arguments
{
    /// <summary>
    /// Represents a text argument.
    /// </summary>
    public sealed class TextArgument : IProcessArgument
    {
        private readonly string _text;
        private readonly IProcessArgumentRenderer _renderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextArgument"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextArgument(string text)
            : this(text, StandardProcessArgumentRenderer.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextArgument"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="renderer">The renderer which handles quoting and escaping. For raw output, pass null.</param>
        public TextArgument(string text, IProcessArgumentRenderer renderer)
        {
            _text = text;
            _renderer = renderer;
        }

        /// <summary>
        /// Render the arguments as a <see cref="System.String" />.
        /// Sensitive information will be included.
        /// </summary>
        /// <returns>
        /// A string representation of the argument.
        /// </returns>
        public string Render()
        {
            return (_renderer != null ? _renderer.Render(_text) : _text) ?? string.Empty;
        }

        /// <summary>
        /// Renders the argument as a <see cref="System.String" />.
        /// Sensitive information will be redacted.
        /// </summary>
        /// <returns>
        /// A safe string representation of the argument.
        /// </returns>
        public string RenderSafe()
        {
            return Render();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return RenderSafe();
        }
    }
}