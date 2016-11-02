using Cake.Core.IO;
using Cake.Core.IO.Arguments;
using Xunit;

namespace Cake.Core.Tests.Unit.IO
{
    public static class StandardProcessArgumentTests
    {
        public sealed class StandardProcessArgumentRendererAutoTests : AutoQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return new StandardProcessArgumentRenderer(ProcessArgumentQuoting.Auto).Render(rawArgument);
            }
        }

        public sealed class StandardProcessArgumentRendererAlwaysTests : AlwaysQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return new StandardProcessArgumentRenderer(ProcessArgumentQuoting.Always).Render(rawArgument);
            }
        }

        public sealed class StandardProcessArgumentRendererNeverTests : AlwaysQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return new StandardProcessArgumentRenderer(ProcessArgumentQuoting.NeverAndThrow).Render(rawArgument);
            }
        }

        public sealed class DefaultStandardProcessArgumentRendererTests : AutoQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return StandardProcessArgumentRenderer.Default.Render(rawArgument);
            }
        }

        public sealed class TextArgumentTests : AutoQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return new TextArgument(rawArgument).Render();
            }
        }

        public sealed class ProcessArgumentBuilderAppendTests : AutoQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return new ProcessArgumentBuilder().Append(rawArgument).Render();
            }
        }

        public sealed class ProcessArgumentBuilderPrependTests : AutoQuotingTestRunner
        {
            public override string Run(string rawArgument)
            {
                return new ProcessArgumentBuilder().Prepend(rawArgument).Render();
            }
        }

        public abstract class AutoQuotingTestRunner
        {
            public abstract string Run(string rawArgument);

            [Fact]
            public void Should_Auto_Quote_Null()
            {
                Assert.Equal("\"\"", Run(null));
            }

            [Fact]
            public void Should_Auto_Quote_Empty()
            {
                Assert.Equal("\"\"", Run(string.Empty));
            }

            [Fact]
            public void Should_Not_Auto_Quote_Simple()
            {
                Assert.Equal("123", Run("123"));
            }

            [Fact]
            public void Should_Not_Auto_Quote_Simple_Ending_Backslash()
            {
                Assert.Equal("123\\", Run("123\\"));
            }

            [Fact]
            public void Should_Auto_Quote_Spaces_Ending_Backslash()
            {
                Assert.Equal("\" 1 2 3 \\\\\"", Run(" 1 2 3 \\"));
            }

            [Fact]
            public void Should_Auto_Quote_Spaces()
            {
                Assert.Equal("\" 1 2 3 \"", Run(" 1 2 3 "));
            }

            [Fact]
            public void Should_Auto_Quote_Quotes()
            {
                Assert.Equal("\"\\\"1\\\"2\\\"3\\\"\"", Run("\"1\"2\"3\""));
            }

            [Fact]
            public void Should_Not_Auto_Quote_Slashes()
            {
                Assert.Equal("1\\2\\\\3\\\\\\", Run("1\\2\\\\3\\\\\\"));
            }

            [Fact]
            public void Should_Auto_Quote_Slashes_Followed_By_Quote()
            {
                Assert.Equal("\"\\\\\\\\\\\"\"", Run("\\\\\""));
            }
        }

        public abstract class AlwaysQuotingTestRunner
        {
            public abstract string Run(string rawArgument);

            [Fact]
            public void Should_Quote_Null_When_Forced()
            {
                Assert.Equal("\"\"", Run(null));
            }

            [Fact]
            public void Should_Quote_Empty_When_Forced()
            {
                Assert.Equal("\"\"", Run(string.Empty));
            }

            [Fact]
            public void Should_Quote_Simple_When_Forced()
            {
                Assert.Equal("\"123\"", Run("123"));
            }

            [Fact]
            public void Should_Quote_Simple_Ending_Backslash_When_Forced()
            {
                Assert.Equal("\"123\\\\\"", Run("123\\"));
            }

            [Fact]
            public void Should_Quote_Slashes_When_Forced()
            {
                Assert.Equal("\"1\\2\\\\3\\\\\\\\\\\\\"", Run("1\\2\\\\3\\\\\\"));
            }
        }

        public abstract class NeverQuotingTestRunner
        {
            public abstract string Run(string rawArgument);

            [Fact]
            public void Should_Throw_For_Null()
            {
                Assert.Throws<InvalidUnquotedArgumentException>(() => Run(null));
            }

            [Fact]
            public void Should_Throw_For_Empty()
            {
                Assert.Throws<InvalidUnquotedArgumentException>(() => Run(string.Empty));
            }

            [Fact]
            public void Should_Throw_For_Simple()
            {
                Assert.Throws<InvalidUnquotedArgumentException>(() => Run("123"));
            }

            [Fact]
            public void Should_Throw_For_Simple_Ending_Backslash()
            {
                Assert.Throws<InvalidUnquotedArgumentException>(() => Run("123\\"));
            }

            [Fact]
            public void Should_Throw_For_Slashes()
            {
                Assert.Throws<InvalidUnquotedArgumentException>(() => Run("1\\2\\\\3\\\\\\"));
            }
        }
    }
}
