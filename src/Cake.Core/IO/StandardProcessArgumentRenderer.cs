using System.Text;

namespace Cake.Core.IO
{
    /// <summary>
    /// Escapes arbitrary values so that the process receives the exact string you intend and argument injection is impossible.
    /// Implements the ubiquitous argv standard. See the MSDN spec for CommandLineToArgvW at https://msdn.microsoft.com/en-us/library/windows/desktop/bb776391.aspx.
    /// </summary>
    public sealed class StandardProcessArgumentRenderer : IProcessArgumentRenderer
    {
        /// <summary>Gets the default quoting mode (<see cref="ProcessArgumentQuoting.Auto"/>).</summary>
        public static IProcessArgumentRenderer Default { get; } = new StandardProcessArgumentRenderer(ProcessArgumentQuoting.Auto);

        private readonly ProcessArgumentQuoting _mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardProcessArgumentRenderer"/> class operating in the given mode. (For <see cref="ProcessArgumentQuoting.Auto"/>, use <see cref="Default"/>.)
        /// </summary>
        /// <param name="mode">Specifies the quoting mode to use.</param>
        public StandardProcessArgumentRenderer(ProcessArgumentQuoting mode)
        {
            _mode = mode;
        }

        private static readonly char[] _charsThatRequireQuoting = { ' ', '"' };
        private static readonly char[] _charsThatRequireEscaping = { '\\', '"' };

        /// <summary>
        /// Transforms the process argument before joining with others.
        /// </summary>
        /// <param name="rawArgument">Arbitrary text to be seen by the process as a single argument.</param>
        /// <returns>The transformed argument ready to be joined with other arguments.</returns>
        public string Render(string rawArgument)
        {
            if (string.IsNullOrEmpty(rawArgument))
            {
                if (_mode == ProcessArgumentQuoting.NeverAndThrow)
                {
                    throw new InvalidUnquotedArgumentException("A blank argument cannot be rendered without quotes because the absence could interfere with the interpretation of following arguments.");
                }
                return "\"\"";
            }

            if (rawArgument.IndexOfAny(_charsThatRequireQuoting) == -1)
            {
                // Happy path
                switch (_mode)
                {
                    case ProcessArgumentQuoting.Auto:
                        return rawArgument;
                    case ProcessArgumentQuoting.NeverAndThrow:
                        throw new InvalidUnquotedArgumentException("An argument containing a space or a quote must be quoted because it could interfere with the interpretation of following arguments.");
                }

                if (rawArgument[rawArgument.Length - 1] != '\\')
                {
                    return "\"" + rawArgument + "\"";
                }
            }

            var sb = new StringBuilder(rawArgument.Length + 8).Append('"');

            var nextPosition = 0;
            while (true)
            {
                var nextEscapeChar = rawArgument.IndexOfAny(_charsThatRequireEscaping, nextPosition);
                if (nextEscapeChar == -1)
                {
                    break;
                }

                sb.Append(rawArgument, nextPosition, nextEscapeChar - nextPosition);
                nextPosition = nextEscapeChar + 1;

                switch (rawArgument[nextEscapeChar])
                {
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        var numBackslashes = 1;
                        while (nextPosition < rawArgument.Length && rawArgument[nextPosition] == '\\')
                        {
                            numBackslashes++;
                            nextPosition++;
                        }
                        if (nextPosition == rawArgument.Length || rawArgument[nextPosition] == '"')
                        {
                            numBackslashes <<= 1;
                        }

                        for (; numBackslashes != 0; numBackslashes--)
                        {
                            sb.Append('\\');
                        }
                        break;
                }
            }

            sb.Append(rawArgument, nextPosition, rawArgument.Length - nextPosition).Append('"');
            return sb.ToString();
        }
    }
}