namespace Cake.Core.IO
{
    /// <summary>
    /// Transforms the process argument before joining with others.
    /// The standard implementation is <see cref="StandardProcessArgumentRenderer"/>.
    /// </summary>
    public interface IProcessArgumentRenderer
    {
        /// <summary>
        /// Transforms the process argument before joining with others.
        /// </summary>
        /// <param name="rawArgument">Arbitrary text to be seen by the process as a single argument.</param>
        /// <returns>The transformed argument ready to be joined with other arguments.</returns>
        string Render(string rawArgument);
    }
}
