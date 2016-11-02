namespace Cake.Core.IO
{
    /// <summary>Modes of operation for standard process argument rendering.</summary>
    public enum ProcessArgumentQuoting
    {
        /// <summary>If the argument does not contain a space or a quote and is not empty, renders as-is. Otherwise quotes and escapes.</summary>
        Auto = 0,

        /// <summary>Always quotes and escapes, even if the argument does not contain a space or a quote.</summary>
        Always,

        /// <summary>Never quotes and escapes; if the argument does contain a space or a quote or is empty, <see cref="InvalidUnquotedArgumentException"/> will be thrown. (If you're trying to output raw arbitrary text, use a null renderer.)</summary>
        NeverAndThrow
    }
}